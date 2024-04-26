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
            string insertString = "MERGE INTO [Statistics].Team AS Target" +
                "USING (SELECT '" + TeamName + "' AS TeamName, '" + ConferenceName + "' AS ConferenceName) AS Source" +
                "ON Target.TeamName = Source.TeamName AND Target.ConferenceName = Source.ConferenceName" +
                "WHEN NOT MATCHED THEN" +
                "    INSERT ([TeamName], ConferenceName)" +
                "    VALUES (Source.TeamName, Source.ConferenceName);";
            SqlCommand comm = new SqlCommand(insertString, connection);
            if (TeamName != "")
            {
                try
                {

                    comm.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }


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
                t.Verified = reader.GetInt32(3);
                Teams.Add(t);
            }
            reader.Close();
            connection.Close();
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
