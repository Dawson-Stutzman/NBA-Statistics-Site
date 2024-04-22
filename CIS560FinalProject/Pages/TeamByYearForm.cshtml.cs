using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CIS560FinalProject.Pages
{
    public class TeamByYearFormModel : PageModel
    {
        public List<TeamInfo> TeamList = new();
        public void OnGet()
        {
            int whereCount = 0;
            string selectString = "SELECT * FROM NBA.[Statistics].[Team] T";
            string teamInput = HttpContext.Request.Query["teams"].ToString();
            if ((HttpContext.Request.Query["allTeams"].Count == 1) || (teamInput.Length == 0))
            {

            }
            else
            {
                //Used to convert "a, b, c" into "'a', 'b', 'c'" so that sql will accept it
                teamInput = string.Join(",", teamInput.Split(',').Select(x => $"'{x.Trim()}'"));
                if (whereCount > 0) { }
                else
                {
                    selectString += " WHERE T.Name IN (" + teamInput + ")";
                }
                Console.WriteLine(HttpContext.Request.Query["teams"].ToString());
                whereCount += 1;
            }
            string conf = HttpContext.Request.Query["conference"].ToString();
            switch (conf)
            {
                case "eastern":
                    if (whereCount > 0) selectString += " AND T.ConferenceName = N'Eastern'";
                    else selectString += " WHERE T.ConferenceName = N'Eastern'";
                    whereCount += 1;
                    break;
                case "western":
                    if (whereCount > 0) selectString += " AND T.ConferenceName = N'Western'";
                    else selectString += " WHERE T.ConferenceName = N'Western'";
                    whereCount += 1;
                    break;
                default:
                    break;
            }
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");

            connection.Open();

            SqlCommand comm = new SqlCommand(selectString, connection);
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                TeamInfo team = new TeamInfo();
                team.TeamID = reader.GetInt32(0);
                team.Name = reader.GetString(1);
                team.ConferenceName = reader.GetString(2);
                TeamList.Add(team);

            }
            foreach (var t in TeamList)
            {
                Console.WriteLine(t.Name);
            }
            connection.Close();
        }
        public class TeamInfo
        {
            public int TeamID;
            public string Name;
            public string ConferenceName;
        }

    }
}
