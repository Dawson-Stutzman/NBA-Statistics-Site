using CIS560FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace CIS560FinalProject.Pages.CustomData
{
    public class AddPlayerSeasonModel : PageModel
    {
        public List<PlayerSeason> PlayerSeasons = new();
        public List<Player> Players = new();
        public List<Team> Teams = new();
        public void OnGet()
        {
            string GamesPlayed = HttpContext.Request.Query["GamesPlayed"].ToString();
            string Minutes = HttpContext.Request.Query["Minutes"].ToString();
            string Points = HttpContext.Request.Query["Points"].ToString();
            string FieldGoalsMade = HttpContext.Request.Query["fieldGoalsMade"].ToString();
            string FieldGoalAttempts = HttpContext.Request.Query["FieldGoalAttempts"].ToString();
            string ThreePointsMade = HttpContext.Request.Query["ThreePointsMade"].ToString();
            string ThreePointAttempts = HttpContext.Request.Query["ThreePointAttempts"].ToString();
            string Rebounds = HttpContext.Request.Query["Rebounds"].ToString();
            string Assists = HttpContext.Request.Query["Assists"].ToString();
            string Blocks = HttpContext.Request.Query["Blocks"].ToString();
            string Steals = HttpContext.Request.Query["Steals"].ToString();
            string Turnovers = HttpContext.Request.Query["Turnovers"].ToString();
            string PlusMinus = HttpContext.Request.Query["PlusMinus"].ToString();
            string PlayerID = HttpContext.Request.Query["player"].ToString();
            string values = String.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', {13}", PlayerID, GamesPlayed, Minutes, Points, FieldGoalsMade, FieldGoalAttempts, ThreePointsMade, ThreePointAttempts, Rebounds, Assists, Blocks, Steals, Turnovers, PlusMinus);
            string min = "'" + Minutes + "'";
            string poi = "'" + Points + "'";
            string fgm = "'" + FieldGoalsMade + "'";
            string fga = "'" + FieldGoalAttempts + "'";
            string tpm = "'" + ThreePointsMade + "'";
            string tpa = "'" + ThreePointAttempts + "'";
            string re = "'" + Rebounds + "'";
            string ass = "'" + Assists + "'";
            string bl = "'" + Blocks + "'";
            string ste = "'" + Steals + "'";
            string tu = "'" + Turnovers + "'";
            string pm = "'" + PlusMinus + "'";

            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            connection.Open();
            SqlDataReader reader;

            string playerListString = "SELECT P.PlayerID, P.PlayerName, P.[Verified] FROM [Statistics].Player P ORDER BY P.PlayerName ASC";
            SqlCommand getPlayers = new SqlCommand(playerListString, connection);
            reader = getPlayers.ExecuteReader();
            while (reader.Read())
            {
                Player p = new();
                p.PlayerID = reader.GetInt32(0);
                p.Name = reader.GetString(1);
                p.Verified = reader.GetInt32(2);
                Players.Add(p);
            }
            reader.Close();

            string teamListString = "SELECT T.TeamID, T.TeamName, T.Verified FROM [Statistics].Team T ORDER BY T.TeamName DESC";
            SqlCommand getTeams = new SqlCommand(teamListString, connection);
            reader = getTeams.ExecuteReader();
            while ( reader.Read())
            {
                Team t = new();
                t.TeamID = reader.GetInt32(0);
                t.Name = reader.GetString(1);
                t.Verified = reader.GetInt32(2);
                Teams.Add(t);
            }
            reader.Close();

            if (GamesPlayed != "")
            {
                string insertString = "INSERT INTO [Statistics].PlayerSeason " +
                    "([PlayerID], [GamesPlayed], [Minutes], [Points], [FieldGoalMade], [FieldGoalAttempts], [ThreePointMade], [ThreePointAttempts], [Rebounds], [Assists], [Blocks], [Steals], [Turnovers], [PlusMinus])"
                    + "VALUES (" + values + ")";
                SqlCommand comm = new SqlCommand(insertString, connection);
                comm.ExecuteNonQuery();

                string summarizeString = "SELECT * FROM [Statistics].TeamSeason";
                SqlCommand summarize = new SqlCommand(summarizeString, connection);
                reader = summarize.ExecuteReader();
                comm.Dispose();

                while (reader.Read())
                {
                    PlayerSeason ts = new();
                    ts.PlayerSeasonID = reader.GetInt32(0);
                    ts.PlayerID = reader.GetInt32(1);
                    ts.TeamSeasonID = reader.GetInt32(2);
                    ts.GamesPlayed = reader.GetInt32(3);
                    ts.Minutes = reader.GetDecimal(4);
                    ts.Points = reader.GetDecimal(5);
                    ts.FieldGoalsMade = reader.GetDecimal(6);
                    ts.FieldGoalAttempts = reader.GetDecimal(7);
                    ts.ThreePointsMade = reader.GetDecimal(8);
                    ts.ThreePointAttempts = reader.GetDecimal(9);
                    ts.Rebounds = reader.GetDecimal(10);
                    ts.Assists = reader.GetDecimal(11);
                    ts.Blocks = reader.GetDecimal(12);
                    ts.Steals = reader.GetDecimal(13);
                    ts.Turnovers = reader.GetDecimal(14);
                    ts.PlusMinus = reader.GetDecimal(15);
                    ts.Verified = reader.GetInt32(16);
                    PlayerSeasons.Add(ts);
                }
                reader.Close();
                connection.Close();
            }


        }

        public class Player
        {
            public int PlayerID;
            public string Name;
            public int Verified;
        }

        public class Team
        {
            public int TeamID;
            public string Name;
            public int Verified;
        }

        public class PlayerSeason
        {
            public int PlayerSeasonID;
            public int PlayerID;
            public int TeamSeasonID;
            public int GamesPlayed;
            public decimal Minutes;
            public decimal Points;
            public decimal FieldGoalsMade;
            public decimal FieldGoalAttempts;
            public decimal ThreePointsMade;
            public decimal ThreePointAttempts;
            public decimal Rebounds;
            public decimal Assists;
            public decimal Blocks;
            public decimal Steals;
            public decimal Turnovers;
            public decimal PlusMinus;
            public int Verified;
        }
    }
}

