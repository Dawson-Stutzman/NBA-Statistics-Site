namespace CIS560FinalProject.Model
{
    public class TeamSeason
    {
        public int TeamSeasonID { get; }

        public int TeamID { get; }

        public string CoachName { get; }

        public int Year { get; }

        public int TeamSeed { get; }

        public TeamSeason(int tsID, int tID, string name, int year, int seed)
        {
            TeamSeasonID = tsID;
            TeamID = tID;
            CoachName = name;
            Year = year;
            TeamSeed = seed;
        }
    }
}
