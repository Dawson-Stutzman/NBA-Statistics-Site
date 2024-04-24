using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

public class PlayerComparisonFormModel : PageModel
{
    public List<Player> Players;
    public string AllPlayers;
    public string Descending;
    public void OnGet()
    {
        AllPlayers = (HttpContext.Request.Query["allPlayers"].ToString() == "on") ? "checked" : "unchecked";
        Descending = (HttpContext.Request.Query["descending"].ToString() == "on") ? "checked" : "unchecked";
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA_Statistics;Integrated Security=True");
        string checkbox = (HttpContext.Request.Query["allPlayers"].ToString());
        connection.Open();

        #region Format Player Names
        // Original string of names
        string players = (HttpContext.Request.Query["players"].ToString());
        // Split the string into individual names
        string[] names = players.Split(',');
        // Format each name with single quotes
        string[] formattedNames = names.Select(name => $"'{name.Trim()}'").ToArray();
        // Join the formatted names into a single string
        string formattedString = string.Join(", ", formattedNames);
        #endregion

        string rankString;
        string orderBy;
        string groupBy = "GROUP BY P.PlayerID, P.PlayerName, P.Verified";

        switch (HttpContext.Request.Query["metric"].ToString())
        {
            case "points":
                orderBy = " ORDER BY Points DESC";
                rankString = "PS.Points";
                break;
            case "assists":
                orderBy = " ORDER BY Assists DESC";
                rankString = "PS.Assists";
                break;
            case "rebounds":
                orderBy = " ORDER BY Rebounds DESC";
                rankString = "PS.Rebounds";
                break;
            case "blocks":
                orderBy = " ORDER BY Blocks DESC";
                rankString = "PS.Blocks";
                break;
            case "steals":
                orderBy = " ORDER BY Steals DESC";
                rankString = "PS.Steals";
                break;
            case "minutes":
                orderBy = " ORDER BY Minutes DESC";
                rankString = "PS.Minutes";
                break;
            case "turnovers":
                orderBy = " ORDER BY Turnovers DESC";
                rankString = "PS.Turnovers";
                break;
            default:
                orderBy = " ORDER BY Points DESC";
                rankString = "PS.Points";
                break;
        }

        string defaultQuery = "WITH RankedPlayers AS (SELECT P.PlayerName AS [Name]," +
            "   CAST(AVG(PS.Points) AS DECIMAL(10,2)) AS Points," +
            "   CAST(AVG(PS.Assists) AS DECIMAL(10,2)) AS Assists," +
            "   CAST(AVG(PS.Rebounds) AS DECIMAL(10,2)) AS Rebounds," +
            "   CAST(AVG(PS.Blocks) AS DECIMAL(10,2)) AS Blocks," +
            "   CAST(AVG(PS.Steals) AS DECIMAL(10,2)) AS Steals," +
            "   CAST(AVG(PS.Minutes) AS DECIMAL(10,2)) AS [Minutes]," +
            "   CAST(AVG(PS.Turnovers) AS DECIMAL(10,2)) AS Turnovers," +
            "   P.Verified," +
            "   ROW_NUMBER() OVER (ORDER BY AVG(" + rankString + ") DESC) AS [Rank]" +
            "       FROM [Statistics].Player P" +
            "           INNER JOIN [Statistics].PlayerSeason PS ON PS.PlayerID = P.PlayerID" +
            "       GROUP BY P.PlayerID, P.PlayerName, P.Verified)" +
            "SELECT [Rank], [Name], Points, Assists, Rebounds, Blocks, Steals, [Minutes], Turnovers, Verified " +
            "FROM RankedPlayers " + orderBy;

        if (players != "" && checkbox == "")
        {
            string where = "WHERE [Name] IN (" + formattedString + ") ";

            defaultQuery = "WITH RankedPlayers AS (SELECT P.PlayerName AS [Name]," +
            "   CAST(AVG(PS.Points) AS DECIMAL(10,2)) AS Points," +
            "   CAST(AVG(PS.Assists) AS DECIMAL(10,2)) AS Assists," +
            "   CAST(AVG(PS.Rebounds) AS DECIMAL(10,2)) AS Rebounds," +
            "   CAST(AVG(PS.Blocks) AS DECIMAL(10,2)) AS Blocks," +
            "   CAST(AVG(PS.Steals) AS DECIMAL(10,2)) AS Steals," +
            "   CAST(AVG(PS.Minutes) AS DECIMAL(10,2)) AS [Minutes]," +
            "   CAST(AVG(PS.Turnovers) AS DECIMAL(10,2)) AS Turnovers," +
            "   P.Verified," +
            "   ROW_NUMBER() OVER (ORDER BY AVG(" + rankString + ") DESC) AS [Rank]" +
            "       FROM [Statistics].Player P" +
            "           INNER JOIN [Statistics].PlayerSeason PS ON PS.PlayerID = P.PlayerID" +
            "       GROUP BY P.PlayerID, P.PlayerName, P.Verified)" +
            "SELECT [Rank], [Name], Points, Assists, Rebounds, Blocks, Steals, [Minutes], Turnovers, Verified " +
            "FROM RankedPlayers " + where + orderBy;

        }



        if (players != "" || checkbox != "")
        {
            SqlCommand comm = new SqlCommand(defaultQuery, connection);
            SqlDataReader reader = comm.ExecuteReader();
            Players = new();


            while (reader.Read())
            {
                Player p = new Player();
                p.Rank = reader.GetInt64(0);
                p.Name = reader.GetString(1);
                p.Points = reader.GetDecimal(2);
                p.Assists = reader.GetDecimal(3);
                p.Rebounds = reader.GetDecimal(4);
                p.Blocks = reader.GetDecimal(5);
                p.Steals = reader.GetDecimal(6);
                p.Minutes = reader.GetDecimal(7);
                p.Turnovers = reader.GetDecimal(8);
                p.Verified = reader.GetInt32(9);


                Players.Add(p);

            }
        }

        connection.Close();


    }

    public class Player
    {
        public string Name;
        public decimal Points;
        public decimal Assists;
        public decimal Rebounds;
        public decimal Blocks;
        public decimal Steals;
        public decimal Minutes;
        public decimal Turnovers;
        public int Verified;
        public long Rank;

    }
}