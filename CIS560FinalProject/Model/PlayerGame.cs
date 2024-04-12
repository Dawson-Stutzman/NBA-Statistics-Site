namespace CIS560FinalProject.Model
{
    public class PlayerGame
    {
        public int GameId { get; }

        public int PlayerSeasonID { get; }

        public int PlayerGameID { get; }

        public decimal Minutes { get; }
        
        public decimal FieldGoalPercentage { get; }

        public decimal ThreePointerPercentage { get; }

        public decimal FreeThrowPercentage { get; }

        public int Rebounds { get; }

        public int Assists { get; }

        public int Blocks { get; }

        public int Steals { get; }

        public string Verified { get; }

        public PlayerGame(int gID, int psID, int pgID, decimal min, decimal fgPercent, decimal tpPercent, decimal ftPercent, int re, int assist, int block, int steal, string verified)
        {
            GameId = gID;
            PlayerSeasonID = psID;
            PlayerGameID = pgID;
            Minutes = min;
            FieldGoalPercentage = fgPercent;
            ThreePointerPercentage = tpPercent;
            FreeThrowPercentage = ftPercent;
            Rebounds = re;
            Assists = assist;
            Blocks = block;
            Steals = steal;
            Verified = verified;
        }
    }
}
