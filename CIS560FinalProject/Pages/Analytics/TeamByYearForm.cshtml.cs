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
        //public string eastConf;
        //public string westConf;
        public string AllTeams;
        public string Descending;
        public string CustomVals;
        public void OnGet()
        {
            AllTeams = (HttpContext.Request.Query["allTeams"].ToString() == "on") ? "checked" : "";
            Descending = (HttpContext.Request.Query["descending"].ToString() == "on") ? "checked" : "";
            CustomVals = (HttpContext.Request.Query["customVals"].ToString() == "on") ? "checked" : "";
            string lowerYear = HttpContext.Request.Query["startDate"].ToString();
            string higherYear = HttpContext.Request.Query["endDate"].ToString();
            string rankMetric = HttpContext.Request.Query["metric"].ToString();


            string selectString = "SELECT T.TeamName," +
                "\r\n    SUM(COALESCE(G.HomePoints, 0) + COALESCE(G.AwayPoints, 0)) AS TotalPoints," +
                "\r\n    SUM(COALESCE(G.HomeRebounds, 0) + COALESCE(G.AwayRebounds, 0)) AS TotalRebounds," +
                "\r\n\tSUM(COALESCE(G.HomeAssists, 0) + COALESCE(G.AwayAssists, 0)) AS TotalAssists," +
                "\r\n\tSUM(CASE WHEN G.WinnerTeamSeasonID = TS.TeamSeasonID THEN 1 ELSE 0 END) AS TotalWins," +
                "\r\n\tSUM(COALESCE(G.Verified, 0)) AS Verified" +
                "\r\nFROM [Statistics].Team T\r\nLEFT JOIN [Statistics].TeamSeason TS ON T.TeamID = TS.TeamID" +
                "\r\nLEFT JOIN [Statistics].Game G ON TS.TeamSeasonID = G.HomeTeamSeasonID OR TS.TeamSeasonID = G.AwayTeamSeasonID" +
                "\r\nGROUP BY T.TeamName";


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
