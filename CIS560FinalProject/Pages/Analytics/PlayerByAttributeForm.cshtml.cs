using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static CIS560FinalProject.Pages.Analytics.TeamByYearFormModel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CIS560FinalProject.Pages.Analytics
{
    public class PlayerByAttributeFormModel : PageModel
    {
        public List<PlayerInfo> PlayerInfoList = new();
        public string ChosenAttribute;
        public string Descending;
        public string CustomVals;
        public string rankMetric;
        public void OnGet()
        {
            ChosenAttribute = HttpContext.Request.Query["attribute"].ToString();
            if (ChosenAttribute == "")
            {
                ChosenAttribute = "Height";
            }
            Descending = (HttpContext.Request.Query["descending"].ToString() == "on") ? "checked" : "unchecked";
            CustomVals = (HttpContext.Request.Query["customVals"].ToString() == "on") ? "checked" : "unchecked";
            rankMetric = HttpContext.Request.Query["rank"].ToString();
            if (rankMetric == "Metric" || rankMetric == "") rankMetric = ChosenAttribute;
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA_Statistics;Integrated Security=True");
                connection.Open();
                string selectString = String.Format("SELECT DISTINCT P.[{0}] AS [{0}], " +
                                        "CAST(AVG(PS.Points) AS DECIMAL(10,2)) AS Points, " +
                                        "CAST(AVG(PS.Assists) AS DECIMAL(10,2)) AS Assists, " +
                                        "CAST(AVG(PS.Rebounds) AS DECIMAL(10,2)) AS Rebounds, " +
                                        "CAST(AVG(PS.Blocks) AS DECIMAL(10,2)) AS Blocks, " +
                                        "CAST(AVG(PS.Steals) AS DECIMAL(10,2)) AS Steals, " +
                                        "CAST(AVG(PS.Turnovers) AS DECIMAL(10,2)) AS Turnovers, " +
                                        "CAST(AVG(PS.[Minutes]) AS DECIMAL(10,2)) AS [Minutes], " +
                                        "SUM(PS.Verified + P.Verified) AS Verified2 " +
                                        "FROM NBA_Statistics.[Statistics].PlayerSeason PS " +
                                            "INNER JOIN [Statistics].Player P ON P.PlayerID = PS.PlayerID {1}" +
                                        "GROUP BY [{0}] " +
                                        "ORDER BY [{2}] {3}", ChosenAttribute, (CustomVals == "checked") ? "WHERE PS.Verified + P.Verified >= 0" : "WHERE PS.Verified + P.Verified = 0", rankMetric,  Descending == "checked" ? "DESC" : "ASC");
                SqlCommand comm = new SqlCommand(selectString, connection);
            Console.WriteLine(selectString);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    PlayerInfo player = new()
                    {
                        Points = reader.GetDecimal(1),
                        Assists = reader.GetDecimal(2),
                        Rebounds = reader.GetDecimal(3),
                        Blocks = reader.GetDecimal(4),
                        Steals = reader.GetDecimal(5),
                        Turnovers = reader.GetDecimal(6),
                        Minutes = reader.GetDecimal(7),
                        Verified = reader.GetInt32(8),
                    };
                if (ChosenAttribute == "Age") 
                { 
                    player.Attribute = reader.GetDecimal(0).ToString(); 
                }
                else player.Attribute = reader.GetString(0);
                PlayerInfoList.Add(player);
                }
                connection.Close();
        }


        public class PlayerInfo
        {
            public string Attribute;
            public decimal Points;
            public decimal Assists;
            public decimal Rebounds;
            public decimal Blocks;
            public decimal Steals;
            public decimal Turnovers;
            public decimal Minutes;
            public int Verified;
        }
    }
}
