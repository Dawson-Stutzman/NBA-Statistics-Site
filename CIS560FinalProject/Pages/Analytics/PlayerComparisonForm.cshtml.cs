using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace CIS560FinalProject.Pages.Analytics
{

    public class PlayerComparisonFormModel : PageModel
    {
        public List<string> Dummy;
        public void OnGet()
        {
            //================================Collecting Relevant Form Data================================
            string colleges = string.Join(",", HttpContext.Request.Query["colleges"].ToString().Split(',').Select(str => $"'{str.Trim()}'"));
            string allColleges = (HttpContext.Request.Query["allColleges"].ToString() == "on") ? "checked" : "";
            string descending = (HttpContext.Request.Query["descending"].ToString() == "on") ? "checked" : "";
            //================================Parse Data into Appropriate SQL filters================================
            int filters = 0;
            string filterString = String.Format("WHERE P.School IN ({0})", colleges);
            


            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            string selectString = String.Format("SELECT * " +
                                  "FROM [Statistics].PlayerSeason PS" +
                                  "INNER JOIN [Statistics].Player P ON P.PlayerID = PS.PlayerID" +
                                        "{0}" +
                                  "GROUP BY P.SCHOOL" +
                                  "ORDER BY {1}", filterString, (descending == "checked") ? "DESC" : "ASC");
            connection.Open();
            SqlCommand comm = new SqlCommand(selectString, connection);
            SqlDataReader reader = comm.ExecuteReader();
            

            /*
            while (reader.Read())
            {
                TeamInfo team = new TeamInfo();
                team.TeamID = reader.GetInt32(0);
                team.Name = reader.GetString(1);
                team.ConferenceName = reader.GetString(2);
                Console.WriteLine(team.Name.ToString());
            }
            connection.Close();
            */

        }
    }
}