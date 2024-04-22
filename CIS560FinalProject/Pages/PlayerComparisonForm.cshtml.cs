using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace CIS560FinalProject.Pages
{
    public class PlayerComparisonFormModel : PageModel
    {
        public void OnGet()
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            string selectString = "SELECT * " +
                                  "FROM Players P";
            connection.Open();
            SqlCommand comm = new SqlCommand("SELECT * FROM NBA.[Statistics].[Team]", connection);
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                TeamInfo team = new TeamInfo();
                team.TeamID = reader.GetInt32(0);
                team.Name = reader.GetString(1);
                team.ConferenceName = reader.GetString(2);
                Console.WriteLine(team.Name.ToString());
            }
            connection.Close();

            if ((HttpContext.Request.Query["allPlayers"].Count == 1) || (HttpContext.Request.Query["players"].ToString().Length == 0))
            {
                Console.WriteLine("All Selected: "+ HttpContext.Request.Query["allPlayers"].Count.ToString());
                Console.WriteLine("Players: " + HttpContext.Request.Query["players"].Count.ToString());
            }
            else 
            {
                selectString += " WHERE P.Name EXISTS (VALUES " + HttpContext.Request.Query["players"].ToString() + ")";
                Console.WriteLine(selectString);
                Console.WriteLine("All Selected: " + HttpContext.Request.Query["allPlayers"].Count.ToString());
                Console.WriteLine("Players: " + HttpContext.Request.Query["players"].ToString());
            }
        }

        public class TeamInfo {
            public int TeamID;
            public string Name;
            public string ConferenceName;
        }
    }
}