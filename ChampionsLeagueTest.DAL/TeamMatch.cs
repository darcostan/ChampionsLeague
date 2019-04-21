using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeagueTest.DAL
{
    public class TeamMatch
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }

    }
}
