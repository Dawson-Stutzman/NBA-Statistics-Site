using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CIS560FinalProject.Pages.CustomData
{
    public class AddPlayerFormModel : PageModel
    {

        public string PlayerName;


        public string Position;


        public string Height;


        public string Weight;


        public string BirthDate;


        public string School;


        public string Age;

        public List<Player> Players = new();


        public void OnGet()
        {

            PlayerName = HttpContext.Request.Query["playerName"].ToString();
            Position = HttpContext.Request.Query["position"].ToString();
            Height = HttpContext.Request.Query["height"].ToString();
            Weight = HttpContext.Request.Query["weight"].ToString();
            BirthDate = HttpContext.Request.Query["birthdate"].ToString();
            School = HttpContext.Request.Query["school"].ToString();
            Age = HttpContext.Request.Query["Age"].ToString();

            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NBA;Integrated Security=True");
            connection.Open();

            string insertString = String.Format("MERGE INTO [Statistics].Player AS Target " +
                "USING (SELECT '{0}' AS PlayerName) AS Source " +
                "ON Target.PlayerName = Source.PlayerName " +
                "WHEN NOT MATCHED THEN " +
                "    INSERT (PlayerName, Position, Height, [Weight], Birthdate, School, Age) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');", PlayerName, Position, Height, Weight, BirthDate, School, Age);
            if (PlayerName != "")
            {
                SqlCommand comm = new SqlCommand(insertString, connection);
                comm.ExecuteNonQuery();

                string summarizeString = "SELECT * FROM [Statistics].Player P ORDER BY P.PlayerName ASC";
                SqlCommand summarize = new SqlCommand(summarizeString, connection);
                SqlDataReader reader = summarize.ExecuteReader();
                comm.Dispose();

                while (reader.Read())
                {
                    Player p = new();
                    p.PlayerID = reader.GetInt32(0);
                    p.PlayerName = reader.GetString(1);
                    p.Position = reader.GetString(2);
                    p.Height = reader.GetString(3);
                    p.Weight = reader.GetString(4);
                    p.BirthDate = reader.GetString(5);
                    p.School = reader.GetString(6);
                    p.Age = reader.GetDecimal(7);
                    p.Verified = reader.GetInt32(8);
                    Players.Add(p);

                }
                reader.Close();
            }

            connection.Close();
        }

        public class Player
        {
            public int PlayerID;
            public string PlayerName;
            public string Position;
            public string Height;
            public string Weight;
            public string BirthDate;
            public string School;
            public decimal Age;
            public int Verified;
        }
    }
}