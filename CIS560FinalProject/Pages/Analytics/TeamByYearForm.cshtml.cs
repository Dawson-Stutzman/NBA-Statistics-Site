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
            AllTeams = (HttpContext.Request.Query["allColleges"].ToString() == "on") ? "checked" : "";
            Descending = (HttpContext.Request.Query["descending"].ToString() == "on") ? "checked" : "";
            CustomVals = (HttpContext.Request.Query["customVals"].ToString() == "on") ? "checked" : "";
            string lowerYear = HttpContext.Request.Query["startDate"].ToString();
            string higherYear = HttpContext.Request.Query["endDate"].ToString();
            string rankMetric = HttpContext.Request.Query["metric"].ToString();


            string selectString = "SELECT TS.TeamSeasonID,\r\nT.TeamName\r\nFROM [Statistics].TeamSeason TS\r\n\tINNER JOIN [Statistics].Team T ON T.TeamID = TS.TeamID\r\nGROUP BY TeamName\r\nORDER BY TeamSeasonID ASC\r\n"


            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            connection.Open();
            SqlCommand comm = new SqlCommand(selectString, connection);
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                TeamInfo t = new()
                {
                    TeamName = reader.GetString(0),
                    Wins = reader.GetInt32(1),
                    Losses = reader.GetInt32(2),
                    Points = reader.GetInt32(3),
                    Assists = reader.GetInt32(4),
                    Rebounds = reader.GetInt32(5),
                    Verified = reader.GetInt32(6),

                };
                TeamList.Add(t);
            }
            connection.Close();


        }
        public class TeamInfo 
        {
            public string TeamName;
            public int Wins;
            public int Losses;
            public int Points;
            public int Assists;
            public int Rebounds;
            public int Verified;
        }
    }
}
