using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CIS560FinalProject.Pages.CustomData
{
    public class AddTeamFormModel : PageModel
    {
        public string TeamName;

        public string ConferenceName;

        public List<Team> Teams = new();
        public void OnGet()
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            connection.Open();
            TeamName = HttpContext.Request.Query["teamName"].ToString();
            ConferenceName = HttpContext.Request.Query["conference"].ToString();
            if(TeamName != "")
            {
                string insertString = "INSERT INTO [Statistics].Team ([TeamName], ConferenceName) VALUES ('" + TeamName + "', '" + ConferenceName + "')";
                SqlCommand comm = new SqlCommand(insertString, connection);
                comm.Parameters.AddWithValue("@TeamName", TeamName);
                comm.Parameters.AddWithValue("@ConferenceName", ConferenceName);
                comm.ExecuteNonQuery();

                string summarizeString = "SELECT * FROM [Statistics].Team";
                SqlCommand summarize = new SqlCommand(summarizeString, connection);
                SqlDataReader reader = summarize.ExecuteReader();
                comm.Dispose();
                
                while (reader.Read())
                {
                    Team t = new();
                    t.TeamID = reader.GetInt32(0);
                    t.Name = reader.GetString(1);
                    t.ConferenceName = reader.GetString(2);
                    t.Verified = 1;
                    Teams.Add(t);
                }
                reader.Close();
                connection.Close();
            }
            
        }

        public class Team
        {
            public int TeamID;
            public string Name;
            public string ConferenceName;
            public int Verified;
        }

    }
}
