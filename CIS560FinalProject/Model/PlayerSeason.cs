namespace CIS560FinalProject.Model
{
    public class PlayerSeason
    {
        public int PlayerSeasonID { get; }

        public int PlayerID { get; }

        public int TeamSeasonID { get; }
        public string Verified { get; }

        public PlayerSeason(int ID, int pID, int teamSeasonID, string verified)
        {
            PlayerSeasonID = ID;
            PlayerID = pID;
            TeamSeasonID = teamSeasonID;
            Verified = verified;

        }

    }
}
