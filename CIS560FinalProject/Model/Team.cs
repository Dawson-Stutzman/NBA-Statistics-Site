namespace CIS560FinalProject.Model
{
    public class Team
    {
        public int TeamID { get; }

        public string TeamName { get; }

        public int ConferenceID { get; }

        public Team(int tID, string name, int cID)
        {
            TeamID = tID;
            TeamName = name;
            ConferenceID = cID;
        }

    }
}
