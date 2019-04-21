using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeagueTest.DAL
{
    public class GroupStanding
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int MachDay { get; set; }
        public List<TeamStanding> TeamStandings { get; set; }
    }
}
