using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using static CIS560FinalProject.Pages.Analytics.MetricsByCollegeFormModel;

namespace CIS560FinalProject.Pages.Analytics
{
    public class TeamByYearFormModel : PageModel
    {
        public List<TeamInfo> TeamList = new();
        public string AllTeams = "";
        public string Descending = "";
        public string CustomVals = "";
        public void OnGet()
        {
            //Collecting data
            string teams = string.Join(",", HttpContext.Request.Query["teams"].ToString().Split(',').Select(str => $"'{str.Trim()}'"));
            AllTeams = (HttpContext.Request.Query["allTeams"].ToString() == "on") ? "checked" : "";
            Descending = (HttpContext.Request.Query["descending"].ToString() == "on") ? "checked" : "";
            CustomVals = (HttpContext.Request.Query["customVals"].ToString() == "on") ? "checked" : "";
            string lowerYear = HttpContext.Request.Query["startDate"].ToString();
            string higherYear = HttpContext.Request.Query["endDate"].ToString();
            string rankMetric = HttpContext.Request.Query["metric"].ToString();
            if (rankMetric == "") rankMetric = "Points";
            //SQL the data
            string dateString = null;
            if (lowerYear != "" && higherYear != "") dateString = "TS.[Year] BETWEEN " + lowerYear + " AND " + higherYear + " ";
            else if (higherYear != "") dateString = "TS.[Year] <= " + higherYear + " ";
            else if (lowerYear != "") dateString = "TS.[Year] >= " + lowerYear + " ";

            string filterString = "";
            int filters = 0;
            if (AllTeams != "checked")
            {
                filterString = String.Format("WHERE T.TeamName IN ({0})", teams);
                filters++;
            }
            else
            {
                if (filters == 0) filterString += "WHERE ";
                else filterString += " AND ";
                string compare;
                if (CustomVals == "checked") compare = " >= 0\n";
                else compare = " = 0\n";

                filterString += "T.[Verified]" + compare;
            }
            if (dateString != null)
            {
                filterString += " AND " + dateString;
            }
            filterString += String.Format("GROUP BY T.TeamName ORDER BY {0} {1}", rankMetric, (Descending == "checked") ? "DESC" : "ASC");


            string selectString = "SELECT T.TeamName," +
                "\r\n    SUM(COALESCE(G.HomePoints, 0) + COALESCE(G.AwayPoints, 0)) AS Points," +
                "\r\n    SUM(COALESCE(G.HomeRebounds, 0) + COALESCE(G.AwayRebounds, 0)) AS Rebounds," +
                "\r\n\tSUM(COALESCE(G.HomeAssists, 0) + COALESCE(G.AwayAssists, 0)) AS Assists," +
                "\r\n\tSUM(CASE WHEN G.WinnerTeamSeasonID = TS.TeamSeasonID THEN 1 ELSE 0 END) AS Wins," +
                "\r\n\tSUM(COALESCE(G.Verified, 0)) AS Verified" +
                "\r\nFROM [Statistics].Team T\r\nLEFT JOIN [Statistics].TeamSeason TS ON T.TeamID = TS.TeamID" +
                "\r\nLEFT JOIN [Statistics].Game G ON TS.TeamSeasonID = G.HomeTeamSeasonID OR TS.TeamSeasonID = G.AwayTeamSeasonID\n" + filterString;


            Console.WriteLine(selectString);

            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            connection.Open();
            SqlCommand comm = new SqlCommand(selectString, connection);
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                TeamInfo t = new()
                {
                    TeamName = reader.GetString(0),
                    Points = reader.GetInt32(1),
                    Rebounds = reader.GetInt32(2),
                    Assists = reader.GetInt32(3),
                    Wins = reader.GetInt32(4),
                    Verified = reader.GetInt32(5),

                };
                TeamList.Add(t);
            }
            connection.Close();
        }
        public class TeamInfo
        {
            public string TeamName;
            public int Wins;
            public int Points;
            public int Assists;
            public int Rebounds;
            public int Verified;
        }
    }
}