using CIS560FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace CIS560FinalProject.Pages.CustomData
{
    public class AddGameFormModel : PageModel
    {

        public List<Team> Teams = new();
        public List<Game> Games = new();
        public void OnGet()
        {

            //======================================================== Form Data Collection ========================================================
            int year = (string.IsNullOrEmpty(HttpContext.Request.Query["year"].ToString())) ? 0 : int.Parse(HttpContext.Request.Query["year"].ToString());

            int homeTeamID = (string.IsNullOrEmpty(HttpContext.Request.Query["homeTeam"].ToString())) ? 0 : int.Parse(HttpContext.Request.Query["homeTeam"].ToString());
            int awayTeamID = (string.IsNullOrEmpty(HttpContext.Request.Query["awayTeam"].ToString())) ? 0 : int.Parse(HttpContext.Request.Query["awayTeam"].ToString());

            int homePoints = (string.IsNullOrEmpty(HttpContext.Request.Query["homePoints"].ToString())) ? 0 : int.Parse(HttpContext.Request.Query["homePoints"].ToString());
            int awayPoints = (string.IsNullOrEmpty(HttpContext.Request.Query["awayPoints"].ToString())) ? 0 : int.Parse(HttpContext.Request.Query["awayPoints"].ToString());

            int homeAssists = (string.IsNullOrEmpty(HttpContext.Request.Query["homeAssists"].ToString())) ? 0 : int.Parse(HttpContext.Request.Query["homeAssists"].ToString());
            int awayAssists = (string.IsNullOrEmpty(HttpContext.Request.Query["awayAssists"].ToString())) ? 0 : int.Parse(HttpContext.Request.Query["awayAssists"].ToString());
            
            int homeRebounds = (string.IsNullOrEmpty(HttpContext.Request.Query["homeRebounds"].ToString())) ? 0 : int.Parse(HttpContext.Request.Query["homeRebounds"].ToString());
            int awayRebounds = (string.IsNullOrEmpty(HttpContext.Request.Query["awayRebounds"].ToString())) ? 0 : int.Parse(HttpContext.Request.Query["awayRebounds"].ToString());

            int winningTeamID = homeTeamID;
            if (homePoints < awayPoints) winningTeamID = awayTeamID;

            int winningTeamSeasonID;
            int homeTeamSeasonID;
            int awayTeamSeasonID;
            //======================================================== Creating Insert Statement ========================================================

            // Check to see if the home and away team TeamSeasonID exist prior to calling on them in the following Query

                SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
                connection.Open();
            SqlDataReader reader;
            if (homeTeamID != 0)
            {
                string getHomeTeamSeasonIDString = String.Format("   SELECT TS.TeamSeasonID " +
                                                    "       FROM [Statistics].TeamSeason TS " +
                                                    "           INNER JOIN [Statistics].Team T ON T.TeamID = {0}" +
                                                    "   WHERE T.TeamID = {0} AND TS.[Year] = {1}", homeTeamID, year);

                string getAwayTeamSeasonIDString = String.Format("   SELECT TS.TeamSeasonID " +
                                        "       FROM [Statistics].TeamSeason TS " +
                                        "           INNER JOIN [Statistics].Team T ON T.TeamID = {0}" +
                                        "   WHERE T.TeamID = {0} AND TS.[Year] = {1}", homeTeamID, year);


                SqlCommand getHomeTeamSeasonID = new SqlCommand(getHomeTeamSeasonIDString, connection);
                SqlCommand getAwayTeamSeasonID = new SqlCommand(getAwayTeamSeasonIDString, connection);
                //If the HomeTeamSeasonID doesn't exist, insert it.
                reader = getHomeTeamSeasonID.ExecuteReader();
                if (reader.Read())
                {
                    homeTeamSeasonID = reader.GetInt32(0);
                }
                else
                {
                    reader.Close();
                    string insertHomeString = String.Format("INSERT [Statistics].TeamSeason (TeamID, [Year]) " +
                        "                                   VALUES ({0}, {1});", homeTeamID, year);
                    SqlCommand insertHomeTeamSeasonID = new SqlCommand(insertHomeString, connection);
                    insertHomeTeamSeasonID.ExecuteNonQuery();

                    SqlCommand home = new SqlCommand("SELECT TS.TeamSeasonID FROM [Statistics].TeamSeason TS INNER JOIN [Statistics].Team T ON T.TeamID = TS.TeamID WHERE T.[TeamID] = " + homeTeamID + " AND TS.[Year] = " + year, connection);
                    reader = home.ExecuteReader();

                    homeTeamSeasonID = reader.GetInt32(0);

                }

                //If the HomeTeamSeasonID doesn't exist, insert it.
                reader.Close();
                reader = getAwayTeamSeasonID.ExecuteReader();

                if (reader.Read())
                {
                    awayTeamSeasonID = reader.GetInt32(0);
                }
                else
                {
                    reader.Close();
                    string insertAwayString = String.Format("INSERT [Statistics].TeamSeason (TeamID, [Year]) " +
                        "                                   VALUES ({0}, {1});", awayTeamID, year);

                    SqlCommand insertAwayTeamSeasonID = new SqlCommand(insertAwayString, connection);
                    insertAwayTeamSeasonID.ExecuteNonQuery();

                    SqlCommand away = new SqlCommand("SELECT TS.TeamSeasonID FROM [Statistics].TeamSeason TS INNER JOIN [Statistics].Team T ON T.TeamID = TS.TeamID WHERE T.[TeamID] = " + awayTeamID + " AND TS.[Year] = " + year, connection);
                    reader = away.ExecuteReader();

                    awayTeamSeasonID = reader.GetInt32(0);

                }
                reader.Close();
                winningTeamSeasonID = (winningTeamID == homeTeamID) ? homeTeamSeasonID : awayTeamSeasonID;

                string insertString = String.Format("INSERT [Statistics].Game(" +
                                                    "HomeTeamSeasonID, " +
                                                    "AwayTeamSeasonID, " +
                                                    "WinnerTeamSeasonID, " +
                                                    "AwayPoints, " +
                                                    "HomePoints, " +
                                                    "AwayAssists, " +
                                                    "HomeAssists, " +
                                                    "AwayRebounds, " +
                                                    "HomeRebounds " +
                                                    "VALUES((" +
                                                    "   {0}, " +
                                                    "   {1}, " +
                                                    "   {2}, " +
                                                    "   {3}, " +
                                                    "   {4}, " +
                                                    "   {5}, " +
                                                    "   {6}, " +
                                                    "   {7}, " +
                                                    "   {8});", homeTeamSeasonID, awayTeamSeasonID, winningTeamSeasonID, homePoints, awayPoints, homeAssists, awayAssists, homeRebounds, awayRebounds);


                //======================================================== Inserting Data ========================================================


                SqlCommand insertGame = new SqlCommand(insertString, connection);
                insertGame.ExecuteNonQuery();
            }
            string teamListString = "SELECT T.TeamID, T.TeamName, T.Verified FROM [Statistics].Team T ORDER BY T.TeamName";
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

        }

        public class Team
        {
            public int TeamID;
            public string Name;
            public int Verified;
        }
        public class Game
        {
            public int HomeTeamSeasonID;
            public int AwayTeamSeasonID;
            public string HomeTeamName;
            public string AwayTeamName;
            public int HomePoints;
            public int AwayPoints;
            public int HomeAssists;
            public int AwayAssists;
            public int HomeRebounds;
            public int AwayRebounds;
            public int Verified;
        }
    }
}
