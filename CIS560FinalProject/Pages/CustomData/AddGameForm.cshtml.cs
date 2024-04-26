using CIS560FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace CIS560FinalProject.Pages.CustomData
{
    public class AddGameFormModel : PageModel
    {

        public List<Team> Teams;
        public List<Game> Games;
        public async void OnGet()
        {
            Teams = new();
            Games = new();


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
            else winningTeamID = homeTeamID;
            int winningTeamSeasonID;
            int homeTeamSeasonID;
            int awayTeamSeasonID;
            //======================================================== Creating Insert Statement ========================================================

            // Check to see if the home and away team TeamSeasonID exist prior to calling on them in the following Query

                SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
                connection.Open();
            SqlDataReader reader;

            string teamListString = "SELECT T.TeamID, T.TeamName, T.Verified FROM [Statistics].Team T ORDER BY T.TeamName DESC";
            SqlCommand getTeams = new SqlCommand(teamListString, connection);
            reader = getTeams.ExecuteReader();
            while (await reader.ReadAsync())
            {
                Team t = new();
                t.TeamID = reader.GetInt32(0);
                t.Name = reader.GetString(1);
                t.Verified = reader.GetInt32(2);
                Teams.Add(t);
            }
            reader.Close();


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
                reader = await getHomeTeamSeasonID.ExecuteReaderAsync();
                if (reader.Read())
                {
                    homeTeamSeasonID = reader.GetInt32(0);
                    reader.Close();
                }
                else
                {
                    reader.Close();
                    connection.Close();
                    connection.Open();
                    year = InsertTeamSeason(homeTeamID, year);

                    SqlCommand home = new SqlCommand(("SELECT TS.TeamSeasonID " +
                        "FROM [Statistics].TeamSeason TS " +
                        "   INNER JOIN [Statistics].Team T ON T.TeamID = TS.TeamID " +
                        "WHERE T.[TeamID] = " + homeTeamID + " AND TS.[Year] = " + year + ""), connection);
                    reader = await home.ExecuteReaderAsync();


                    homeTeamSeasonID = reader.GetInt16(0);
                    reader.Close();
                }

                //If the HomeTeamSeasonID doesn't exist, insert it.

                reader = getAwayTeamSeasonID.ExecuteReader();

                if (reader.Read())
                {
                    awayTeamSeasonID = reader.GetInt32(0);
                    reader.Close();
                }
                else
                {
                    reader.Close();
                    connection.Close();
                    connection.Open();
                    InsertTeamSeason(awayTeamID, year);
                    SqlCommand away = new SqlCommand(("SELECT TS.TeamSeasonID FROM [Statistics].TeamSeason TS INNER JOIN [Statistics].Team T ON T.TeamID = TS.TeamID WHERE T.[TeamID] = " + awayTeamID + " AND TS.[Year] = " + year + ""), connection);
                    reader = await away.ExecuteReaderAsync();

                    awayTeamSeasonID = reader.GetInt32(0);
                    reader.Close();
                }

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
                                                    "HomeRebounds" +
                                                    ")VALUES(" +
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

                Console.WriteLine(insertString);
                SqlCommand insertGame = new SqlCommand(insertString, connection);
                await insertGame.ExecuteNonQueryAsync();
            }


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

        public int InsertTeamSeason(int TeamID, int year)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            connection.Open();
            SqlDataReader reader;
            string insertHomeString = String.Format("INSERT [Statistics].TeamSeason (TeamID, [Year]) " +
    "                                   VALUES ({0}, {1});", TeamID, year);
            SqlCommand insertHomeTeamSeasonID = new SqlCommand(insertHomeString, connection);
            insertHomeTeamSeasonID.ExecuteNonQuery();
            insertHomeTeamSeasonID.Dispose();
            connection.Close();
            return year;
        }
    }
}
