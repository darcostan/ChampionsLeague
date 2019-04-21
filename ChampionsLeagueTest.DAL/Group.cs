using System.Collections.Generic;

namespace ChampionsLeagueTest.DAL
{
    public class Group
    {
        public Group()
        {
            Teams = new List<Team>(4);
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<Team> Teams { get; set; }
        public List<Match> Matches { get; set; }
        public List<GroupStanding> GroupStandings { get; set; }

    }
}