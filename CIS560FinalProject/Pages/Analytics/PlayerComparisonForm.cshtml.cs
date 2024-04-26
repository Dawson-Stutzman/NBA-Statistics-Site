using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

public class PlayerComparisonFormModel : PageModel
{
    public List<Player> Players;
    public string AllPlayers;
    public string Descending;
    public string CustomVals;
    public void OnGet()
    {
        AllPlayers = (HttpContext.Request.Query["allPlayers"].ToString() == "on") ? "checked" : "unchecked";
        Descending = (HttpContext.Request.Query["descending"].ToString() == "on") ? "checked" : "unchecked";
        CustomVals = (HttpContext.Request.Query["customVals"].ToString() == "on") ? "checked" : "unchecked";
        string Desc = (Descending == "checked") ? "ASC" : "DESC";
        SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");

        connection.Open();

        #region Format Player Names
        // Original string of names
        string players = (HttpContext.Request.Query["players"].ToString());
        if (players == "") AllPlayers = "checked";
        // Split the string into individual names
        string[] names = players.Split(',');
        // Format each name with single quotes
        string[] formattedNames = names.Select(name => $"'{name.Trim()}'").ToArray();
        // Join the formatted names into a single string
        string formattedString = string.Join(", ", formattedNames);
        #endregion

        string rankString;
        string orderBy;
        int whereCount = 0;
        string groupBy = "GROUP BY P.PlayerID, P.PlayerName, P.Verified";
        string where = "";

        if (players != "" && AllPlayers == "checked")
        {
            where += "WHERE [Name] IN (" + formattedString + ") ";
            whereCount++;
        }
        if (CustomVals != "checked")
        {
            if (whereCount > 0)
            {
                where += "AND [Verified] = 0";
            }
            else
            {
                where += "WHERE [Verified] = 0";
            }
        }

        switch (HttpContext.Request.Query["metric"].ToString())
        {
            case "points":
                orderBy = " ORDER BY Points " + Desc;
                rankString = "PS.Points";
                break;
            case "assists":
                orderBy = " ORDER BY Assists " + Desc;
                rankString = "PS.Assists";
                break;
            case "rebounds":
                orderBy = " ORDER BY Rebounds " + Desc;
                rankString = "PS.Rebounds";
                break;
            case "blocks":
                orderBy = " ORDER BY Blocks " + Desc;
                rankString = "PS.Blocks";
                break;
            case "steals":
                orderBy = " ORDER BY Steals " + Desc;
                rankString = "PS.Steals";
                break;
            case "minutes":
                orderBy = " ORDER BY Minutes " + Desc;
                rankString = "PS.Minutes";
                break;
            case "turnovers":
                orderBy = " ORDER BY Turnovers " + Desc;
                rankString = "PS.Turnovers";
                break;
            default:
                orderBy = " ORDER BY Points " + Desc;
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
        "   SUM(P.[Verified]) AS [Verified]," +
        "   ROW_NUMBER() OVER (ORDER BY AVG(" + rankString + ")" + Desc + ") AS [Rank]" +
        "       FROM [Statistics].Player P" +
        "           INNER JOIN [Statistics].PlayerSeason PS ON PS.PlayerID = P.PlayerID " +
        "       GROUP BY P.PlayerID, P.PlayerName) " +
        "SELECT [Rank], [Name], Points, Assists, Rebounds, Blocks, Steals, [Minutes], Turnovers, [Verified] " +
        "FROM RankedPlayers " + where + orderBy;





        Console.WriteLine(defaultQuery);
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