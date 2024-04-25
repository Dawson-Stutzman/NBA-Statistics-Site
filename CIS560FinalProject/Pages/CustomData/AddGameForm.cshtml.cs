using CIS560FinalProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CIS560FinalProject.Pages.CustomData
{
    public class AddGameFormModel : PageModel
    {

        public List<Team> Teams = new();
        public List<Game> Games = new();
        public void OnGet()
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            connection.Open();
            string teamListString = "SELECT T.TeamID, T.TeamName, T.Verified FROM [Statistics].Team T ORDER BY T.TeamName";
            SqlCommand getTeams = new SqlCommand(teamListString, connection);
            SqlDataReader reader = getTeams.ExecuteReader();
                
            while (reader.Read())
            {
                Team t = new();
                t.TeamID = reader.GetInt32(0);
                t.Name = reader.GetString(1);
                t.Verified = reader.GetInt32(2);
                Teams.Add(t);
            }
            reader.Close();
            
            
            
            
            
            
            
            
            
            connection.Close();
            
            
        }

        public class Team
        {
            public int TeamID;
            public string Name;
            public int Verified;
        }
        public class Game
        {
            public int HomeTeamSeasonID;
            public int AwayTeamSeasonID;
            public string HomeTeamName;
            public string AwayTeamName;
            public int HomePoints;
            public int AwayPoints;
            public int HomeAssists;
            public int AwayAssists;
            public int HomeRebounds;
            public int AwayRebounds;
            public int Verified;
        }
    }
}
