using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

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

            int homeTeamSeasonID = 0;
            int awayTeamSeasonID = 0;
            int winnerTeamSeasonID = 0;
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


            if (homeTeamID != 0 && awayTeamID != 0 && (homePoints != 0 || awayPoints != 0))
            {
                /*
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
                */
                try
                {
                    string getHomeTeamSeasonIDString = String.Format("   SELECT TS.TeamSeasonID " +
                                        "       FROM [Statistics].TeamSeason TS " +
                                        "           INNER JOIN [Statistics].Team T ON T.TeamID = TS.TeamID" +
                                        "   WHERE T.TeamID = {0} AND TS.[Year] = {1}", homeTeamID, year);


                    SqlCommand getHomeTeamSeasonID = new SqlCommand(getHomeTeamSeasonIDString, connection);

                    reader = getHomeTeamSeasonID.ExecuteReader();
                    reader.Read();

                    homeTeamSeasonID = reader.GetInt32(0);
                    connection.Close();
                }
                catch { }

                try
                {
                    string getAwayTeamSeasonIDString = String.Format("   SELECT TS.TeamSeasonID " +
                    "       FROM [Statistics].TeamSeason TS " +
                    "           INNER JOIN [Statistics].Team T ON T.TeamID = TS.TeamID" +
                    "   WHERE T.TeamID = {0} AND TS.[Year] = {1}", awayTeamID, year);

                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    SqlCommand getAwayTeamSeasonID = new SqlCommand(getAwayTeamSeasonIDString, connection);

                    reader = getAwayTeamSeasonID.ExecuteReader();

                    awayTeamSeasonID = reader.GetInt32(0);


                }
                catch { }
                winnerTeamSeasonID = (winningTeamID == homeTeamID) ? homeTeamSeasonID : awayTeamSeasonID;
                try
                {
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
                                                        "   {8});", homeTeamSeasonID, awayTeamSeasonID, winnerTeamSeasonID, homePoints, awayPoints, homeAssists, awayAssists, homeRebounds, awayRebounds);


                    //======================================================== Inserting Data ========================================================

                    Console.WriteLine(insertString);
                    SqlCommand insertGame = new SqlCommand(insertString, connection);
                    insertGame.ExecuteNonQuery();
                }
                catch { }
            }

            /*string selectString = "SELECT TS.[Year], (SELECT T.TeamName FROM [Statistics].Team T INNER JOIN [Statistics].TeamSeason TS ON  TS.TeamSeasonID = G.HomeTeamSeasonID) AS HomeTeam, " +
                "(SELECT T.TeamName FROM [Statistics].Team T INNER JOIN [Statistics].TeamSeason TS ON  TS.TeamSeasonID = G.AwayTeamSeasonID) AS AwayTeam, " +
                "(SELECT T.TeamName FROM [Statistics].Team T INNER JOIN [Statistics].TeamSeason TS ON  TS.TeamSeasonID = G.WinnerTeamSeasonID) AS WinningTeam, " +
                "G.HomePoints AS HomePoints, G.AwayPoints AS AwayPoints, G.HomeAssists AS HomeAssits, G.AwayAssists AS AwayAssists, G.HomeRebounds AS HomeRebounds, G.AwayRebounds AS AwayRebounds, " +
                "G.Verified + TS.Verified + T.Verified AS Verified " +
                "FROM [Statistics].Game G " +
                "INNER JOIN [Statistics].TeamSeason TS ON TS.TeamSeasonID = G.HomeTeamSeasonID OR TS.TeamSeasonID = G.AwayTeamSeasonID " +
                "INNER JOIN [Statistics].Team T ON T.TeamID = TS.TeamID ";*/

            string selectString = "SELECT\r\n    ht.TeamName AS HomeTeam,\r\n    at.TeamName AS AwayTeam,\r\n    CASE\r\n        WHEN g.WinnerTeamSeasonID = hts.TeamSeasonID THEN ht.TeamName\r\n        ELSE at.TeamName\r\n    END AS WinningTeam,\r\n    hts.Year AS GameYear,\r\n    g.HomePoints,\r\n    g.AwayPoints,\r\n    g.HomeAssists,\r\n    g.AwayAssists,\r\n    g.HomeRebounds,\r\n    g.AwayRebounds,\r\n    g.Verified\r\nFROM\r\n    [Statistics].Game g\r\n    INNER JOIN [Statistics].TeamSeason hts ON g.HomeTeamSeasonID = hts.TeamSeasonID\r\n    INNER JOIN [Statistics].Team ht ON hts.TeamID = ht.TeamID\r\n    INNER JOIN [Statistics].TeamSeason ats ON g.AwayTeamSeasonID = ats.TeamSeasonID\r\n    INNER JOIN [Statistics].Team at ON ats.TeamID = at.TeamID;";
            SqlCommand selectGames = new SqlCommand(selectString, connection);
            reader.Close();
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

            reader = selectGames.ExecuteReader();
            while (reader.Read())
            {
                Game g = new()
                {
                    HomeTeamName = reader.GetString(0),
                    AwayTeamName = reader.GetString(1),
                    WinningTeamName = reader.GetString(2),
                    Year = reader.GetInt32(3),
                    HomePoints = reader.GetInt32(4),
                    AwayPoints = reader.GetInt32(5),
                    HomeAssists = reader.GetInt32(6),
                    AwayAssists = reader.GetInt32(7),
                    HomeRebounds = reader.GetInt32(8),
                    AwayRebounds = reader.GetInt32(9),
                    Verified = reader.GetInt32(10),
                };
                Games.Add(g);
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
            public int Year;
            public int HomeTeamSeasonID;
            public int AwayTeamSeasonID;
            public string HomeTeamName;
            public string AwayTeamName;
            public string WinningTeamName;
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
