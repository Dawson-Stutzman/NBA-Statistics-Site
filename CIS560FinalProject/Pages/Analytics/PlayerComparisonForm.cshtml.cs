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
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            string selectString = "SELECT * " +
                                  "FROM Players P";
            connection.Open();
            SqlCommand comm = new SqlCommand("SELECT * FROM NBA.[Statistics].[Team]", connection);
            SqlDataReader reader = comm.ExecuteReader();
            Dummy = new();
            Dummy.Add("Dummy1");
            Dummy.Add("Dummy2");

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

        public class TeamInfo
        {
            public int TeamID;
            public string Name;
            public string ConferenceName;
        }
    }
}