namespace ChampionsLeagueTest.DAL
{
    public class TeamStanding
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int PlayedGames { get; set; }
        public int Points { get; set; }
        public int Goals { get; set; }
        public int GoalsAgainst { get; set; }

        public int GoalDifference
        {
            get => Goals - GoalsAgainst; 
            set => value = Goals - GoalsAgainst;
        }
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Draw { get; set; }
        public int GroupStandingId { get; set; }
        public GroupStanding GroupStanding { get; set; }
    }
}