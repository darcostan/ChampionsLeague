using System.Collections.Generic;

namespace ChampionsLeagueTest.DAL
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<TeamMatch> TeamMatches { get; set; }
        public List<TeamStanding> TeamStandings { get; set; }
    }
}