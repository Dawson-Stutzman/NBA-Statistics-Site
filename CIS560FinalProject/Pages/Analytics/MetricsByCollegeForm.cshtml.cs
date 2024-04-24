using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;

namespace CIS560FinalProject.Pages.Analytics
{
    public class MetricsByCollegeFormModel : PageModel
    {
        public class CollegeInfo
        {
            public string CollegeName;
            public decimal Points;
            public decimal Assists;
            public decimal Rebounds;
            public decimal Blocks;
            public decimal Steals;
            public decimal Turnovers;
            public decimal Minutes;
            public int Verified;
        }
        public string AllColleges = "";
        public string Descending = "";
        public string CustomVals = "";

        public List<CollegeInfo> CollegeInfoList = new();

        public void OnGet()
        {
            //================================Collecting Relevant Form Data================================
            string colleges = string.Join(",", HttpContext.Request.Query["colleges"].ToString().Split(',').Select(str => $"'{str.Trim()}'"));
            AllColleges = (HttpContext.Request.Query["allColleges"].ToString() == "on") ? "checked" : "";
            Descending = (HttpContext.Request.Query["descending"].ToString() == "on") ? "checked" : "";
            CustomVals = (HttpContext.Request.Query["customVals"].ToString() == "on") ? "checked" : "";
            string rankMetric = HttpContext.Request.Query["metric"].ToString();
            if (rankMetric == "") rankMetric = "School";
            //================================Parse Data into Appropriate SQL filters================================
            string filterString = "";
            int filters = 0;
            if (colleges.Length > 2 && AllColleges != "checked") 
            { 
                filterString = String.Format("WHERE P.School IN ({0})", colleges);
                filters++;
            }
            if (CustomVals == "checked")
            {
                if (filters == 0) filterString += "WHERE ";
                else filterString += "AND ";
                filterString += "PS.[Verified] >= 0";
                filters++;
            }
            else 
            {
                if (filters == 0) filterString += "WHERE ";
                else filterString += "AND ";
                filterString += "PS.[Verified] = 0";
                filters++;
            }
            filterString += String.Format("GROUP BY P.SCHOOL ORDER BY [{0}] {1}", rankMetric, (Descending == "checked") ? "DESC" : "ASC");
            
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            string selectString = String.Format("SELECT P.School AS School, " + 
                                                "SUM(PS.Points) AS Points, " + 
                                                "SUM(PS.Assists) AS Assists, " + 
                                                "SUM(PS.Rebounds) AS Rebounds, " + 
                                                "SUM(PS.Blocks) AS Blocks, " + 
                                                "SUM(PS.Steals) AS Steals, " + 
                                                "SUM(PS.Turnovers) AS Turnovers, " + 
                                                "SUM(PS.[Minutes]) AS [Minutes] " +
                                                "FROM [Statistics].PlayerSeason PS " +
                                                "INNER JOIN [Statistics].Player P ON P.PlayerID = PS.PlayerID " +
                                                    "{0}", filterString);
            connection.Open();
            SqlCommand comm = new SqlCommand(selectString, connection);
            SqlDataReader reader = comm.ExecuteReader();


            
            while (reader.Read())
            {
                CollegeInfo c = new CollegeInfo() { CollegeName = reader.GetString(0), 
                                                    Points = reader.GetDecimal(1),
                                                    Assists = reader.GetDecimal(2),
                                                    Rebounds = reader.GetDecimal(3),
                                                    Blocks = reader.GetDecimal(4),
                                                    Steals = reader.GetDecimal(5),
                                                    Turnovers = reader.GetDecimal(6),
                                                    Minutes = reader.GetDecimal(7),
                                                    };
                CollegeInfoList.Add(c);
            }
            connection.Close();
            

        }

    }
}
