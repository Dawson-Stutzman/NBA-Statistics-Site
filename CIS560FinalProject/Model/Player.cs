namespace CIS560FinalProject.Model
{
    public class Player
    {
        public int PlayerID { get; }
        public string Height { get; }

        public int Weight { get; }

        public DateTime Birthdate { get; }

        public string College { get; }

        //// what
        public string DraftInfo { get; }

        public string Verified { get; }

        public Player(int ID, string height, int weight, DateTime birthdate, string college, string draft, string verified)
        {
            PlayerID = ID;
            Height = height;
            Weight = weight;
            Birthdate = birthdate;
            College = college;
            DraftInfo = draft;
            Verified = verified;
        }
    }
}
