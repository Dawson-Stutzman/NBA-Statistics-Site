using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace CIS560FinalProject.Pages.Analytics
{
    public class TeamByYearFormModel : PageModel
    {
        public List<TeamInfo> TeamList = new();
        //public string eastConf;
        //public string westConf;
        public string allTeams;
        public void OnGet()
        {
            int whereCount = 0;
            string selectString = "SELECT * FROM NBA.[Statistics].[Team] T";
            string teamInput = HttpContext.Request.Query["teams"].ToString();
            teamInput = string.Join(",", teamInput.Split(',').Select(x => $"'{x.Trim()}'"));
            //eastConf = (HttpContext.Request.Query["eastConf"].ToString() == "on") ? "checked" : "unchecked";
            //westConf = (HttpContext.Request.Query["westConf"].ToString() == "on") ? "checked" : "unchecked";
            allTeams = (HttpContext.Request.Query["allTeams"].ToString() == "on") ? "checked" : "unchecked";
            if (HttpContext.Request.Query["allTeams"].Count == 1 || teamInput.Length == 0)
            {

            }
            else
            {
                //Used to convert "a, b, c" into "'a', 'b', 'c'" so that sql will accept it

                if (whereCount > 0) { }
                else
                {
                    selectString += " WHERE T.Name IN (" + teamInput + ")";
                }
                Console.WriteLine(HttpContext.Request.Query["teams"].ToString());
                whereCount += 1;
            }
            string conf = HttpContext.Request.Query["eastConf"].ToString();
            /*if ((eastConf == "checked" && westConf == "checked") || (eastConf != "checked" && westConf != "checked")) { }
            else if (eastConf == "checked")
            {
                if (whereCount > 0) selectString += " AND T.ConferenceName = N'Eastern'";
                else selectString += " WHERE T.ConferenceName = N'Eastern'";
                whereCount += 1;
            }
            else if (westConf == "checked") 
            {
                if (whereCount > 0) selectString += " AND T.ConferenceName = N'Western'";
                else selectString += " WHERE T.ConferenceName = N'Western'";
                whereCount += 1;
            }*/

            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA_Statistics;Integrated Security=True");
            connection.Open();
            SqlCommand comm = new SqlCommand(selectString, connection);
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                TeamInfo team = new TeamInfo();
                team.TeamID = reader.GetInt32(0);
                team.Name = reader.GetString(1);
                team.ConferenceName = reader.GetString(2);
                team.Verified = reader.GetInt32(3);
                TeamList.Add(team);
            }
            connection.Close();
        }
        public class TeamInfo
        {
            public int TeamID;
            public string Name;
            public string ConferenceName;
            public int Verified;
        }

    }
}
