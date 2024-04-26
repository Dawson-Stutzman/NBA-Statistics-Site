using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CIS560FinalProject.Pages.CustomData
{
    public class AddTeamSeasonFormModel : PageModel
    {
        public List<TeamSeason> TeamSeasons = new();
        public List<Team> Teams = new();
        public void OnGet()
        {
            string TeamID;
            string Year;
            SqlDataReader reader;
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            TeamID = HttpContext.Request.Query["team"].ToString();
            Year = HttpContext.Request.Query["year"].ToString();





            
            if (TeamID != "" && Year != "")
            {
                string insertString = String.Format("   MERGE INTO [Statistics].TeamSeason AS Target" +
                    "                                   USING (SELECT {0} AS TeamID, {1} AS Year, 0 AS [Verified]) AS Source" +
                    "                                   ON Target.TeamID = Source.TeamID AND Target.Year = Source.Year" +
                    "                                   WHEN NOT MATCHED THEN" +
                    "                                   INSERT (TeamID, Year)" +
                    "                                   VALUES (Source.TeamID, Source.Year);", TeamID, Year);
                connection.Open();
                SqlCommand comm = new SqlCommand(insertString, connection);
                comm.ExecuteNonQuery();
                string summarizeString = "SELECT * FROM [Statistics].Team";
                SqlCommand summarize = new SqlCommand(summarizeString, connection);
                reader = summarize.ExecuteReader();
                comm.Dispose();
                connection.Close();
            }







            string teamListString = "SELECT T.TeamID, T.TeamName, T.Verified FROM [Statistics].Team T ORDER BY T.TeamName ASC";
            connection.Open();
            SqlCommand getTeams = new SqlCommand(teamListString, connection);
            reader = getTeams.ExecuteReader();
            while (reader.Read())
            {
                Team t = new();
                t.TeamID = reader.GetInt32(0);
                t.Name = reader.GetString(1);
                t.Verified = reader.GetInt32(2);
                Teams.Add(t);
            }
            reader.Close();
            connection.Close();








            string teamSeasonListString = "SELECT T.TeamName, TS.[Year], T.Verified + Ts.Verified FROM [Statistics].Team T INNER JOIN [Statistics].TeamSeason TS ON TS.TeamID = T.TeamID ORDER BY T.TeamName ASC, TS.[Year] DESC";
            connection.Open();
            SqlCommand getTeamSeasons = new SqlCommand(teamSeasonListString, connection);
            reader = getTeamSeasons.ExecuteReader();
            while (reader.Read())
            {
                TeamSeason t = new();
                t.Name = reader.GetString(0);
                t.Year = reader.GetInt32(1);
                t.Verified = reader.GetInt32(2);
                TeamSeasons.Add(t);
            }
            reader.Close();
            connection.Close();
        }

        public class Team
        {
            public int TeamID;
            public string Name;
            public int Verified;
        }

        public class TeamSeason
        {

            public string Name;
            public int Year;
            public int Verified;
        }
    }
}
