using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChampionsLeagueTest.Core.Dto;
using ChampionsLeagueTest.DAL;

namespace ChampionsLeagueTest.Core.Interfaces
{
    public interface IMatchService
    {
        Task<GroupStanding> GetCurrentStandingsAsync();
        Task<List<GroupStanding>> GetCurrentStandingsAsync(int groupId);
        Task<List<GroupStanding>> GetCurrentStandingsAsync(int groupId, int matchDay);

        Task<GroupStanding> LoadMatchesAsync(MatchDto matches);
        Task<List<GroupStanding>> LoadMatchesAsync(List<MatchDto> matches);

        Task<List<Match>> ListMatches(string time, string timeTo, string groupName, string teamName );
        Task<Match> ListMatches(int id);
    }
}