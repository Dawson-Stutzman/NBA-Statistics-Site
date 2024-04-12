namespace CIS560FinalProject.Model
{
    public class Game
    {
        public int GameID { get; }
        public int HomeTeamID { get; }
        public int AwayTeamID { get; }

        //easier if its just an int instead of a datetime
        public int Year { get; }

        public DateTime Date { get; }

        public string Verified { get; }

        public Game(int gID, int htID, int atID, int year, DateTime date, string verified)
        {
            GameID = gID;
            HomeTeamID = htID;
            AwayTeamID = atID;
            Year = year;
            Date = date;
            Verified = verified;
        }
    }
}
