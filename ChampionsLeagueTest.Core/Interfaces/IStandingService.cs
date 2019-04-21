using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChampionsLeagueTest.DAL;

namespace ChampionsLeagueTest.Core.Interfaces
{
    public interface IStandingService
    {
        Task<GroupStanding> CurrentStandings(Match match);
    }
}
