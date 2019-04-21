using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChampionsLeagueTest.Core.Dto;
using ChampionsLeagueTest.Core.Interfaces;
using ChampionsLeagueTest.DAL;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeagueTest.Core.BusinessLogic
{
    public class MatchService : IMatchService
    {
        private readonly ChampionsLeagueTestDbContext _db;
        private readonly IStandingService _standingService;
        public MatchService(ChampionsLeagueTestDbContext db, IStandingService service)
        {
            _db = db;
            _standingService = service;
        }
        public async Task<GroupStanding> GetCurrentStandingsAsync()
        {
            var result = await _db.GroupStandings.OrderByDescending(g => g.MachDay)
                .Include(g => g.Group)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<GroupStanding>> GetCurrentStandingsAsync(int groupId)
        {
            var result = await _db.GroupStandings.Where(g => g.GroupId == groupId).ToListAsync();

            if (result.Any())
            {
                return result;
            }

            return null;
        }

        public async Task<List<GroupStanding>> GetCurrentStandingsAsync(int groupId, int matchDay)
        {
            var result = await _db.GroupStandings.Where(g => g.GroupId == groupId && g.MachDay == matchDay).ToListAsync();

            if (result.Any())
            {
                return result;
            }

            return null;
        }

        public async Task<GroupStanding> LoadMatchesAsync(MatchDto matchDto)
        {
            try
            {
                var group = await _db.Groups.SingleOrDefaultAsync(g => g.GroupName == matchDto.Group);

                if (group is null)
                {
                    return null;
                }

                var homeTeam = await _db.Teams.SingleOrDefaultAsync(t => t.Name == matchDto.HomeTeam);

                if (homeTeam is null)
                {
                    return null;
                }

                var awayTeam = await _db.Teams.SingleOrDefaultAsync(t => t.Name == matchDto.AwayTeam);

                if (awayTeam is null)
                {
                    return null;
                }

                DateTime.TryParse(matchDto.KickoffAt, out var dateTime);
                
                var match = new Match
                {
                    GroupId = group.Id,
                    Group = group,
                    HomeTeam = homeTeam.Name,
                    AwayTeam = awayTeam.Name,
                    LeagueTitle = matchDto.LeagueTitle,
                    MatchDay = matchDto.MatchDay,
                    KickOffAt = dateTime
                };

                var score = Match.GetResult(matchDto.Score);

                if (score.homeGoals == -1 || score.awayGoals == -1)
                {
                    return null;
                }

                match.GoalsHomeTeam = score.homeGoals;
                match.GoalsAwayTeam = score.awayGoals;

                _db.Matches.Add(match);
                _db.SaveChanges();

                var teamMatches = new List<TeamMatch>
                {
                    new TeamMatch() {TeamId = homeTeam.Id, MatchId = match.Id},
                    new TeamMatch() {TeamId = awayTeam.Id, MatchId = match.Id}
                };

                _db.TeamMatches.AddRange(teamMatches);
                await _db.SaveChangesAsync();
                
                var stand = await _standingService.CurrentStandings(match);

                return stand;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<GroupStanding>> LoadMatchesAsync(List<MatchDto> matches)
        {
            var result = new List<GroupStanding>();

            foreach (var match in matches)
            {
                var resultOne = await LoadMatchesAsync(match);

                if (resultOne != null && !result.Contains(resultOne))
                {
                    result.Add(resultOne);
                }
            }

            return result;
        }

        public async Task<List<Match>> ListMatches(string fromDate = null, string toDate = null, string groupName = null, string teamName = null)
        {
            try
            {
                if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(groupName) && string.IsNullOrEmpty(teamName))
                {
                    return await _db.Matches.Include(m => m.Group).ToListAsync();
                }

                List<Match> result = null;

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    if (DateTime.TryParse(fromDate, out var fromKickOffAt) && DateTime.TryParse(toDate, out var toKickOffAt))
                    {
                        result = await _db.Matches.Where(m => m.KickOffAt <fromKickOffAt && m.KickOffAt < toKickOffAt).Include(m => m.Group).ToListAsync();
                    }
                }

                if (!string.IsNullOrEmpty(groupName))
                {
                    var group = await _db.Groups.SingleOrDefaultAsync(g => g.GroupName == groupName);

                    if (result is null)
                    {
                        result = await _db.Matches.Where(m => m.GroupId == group.Id).Include(m => m.Group).ToListAsync();
                    }
                    else
                    {
                        result = result.Where(m => m.GroupId == group.Id).ToList();
                    }

                }

                if(!string.IsNullOrEmpty(teamName))
                {
                    var team = await _db.Teams.Where(t => t.Name == teamName).Include(t => t.TeamMatches).SingleOrDefaultAsync();

                    var matchesIds = team.TeamMatches?.Select(t => t.MatchId).ToList();

                    if (result is null)
                    {
                        result = await _db.Matches.Where(m => matchesIds.Contains(m.Id)).Include(m => m.Group).ToListAsync();
                    }
                    else
                    {
                        if (matchesIds != null && matchesIds.Any())
                        {
                            result = result.Where(m => matchesIds.Contains(m.Id)).ToList();
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Match> ListMatches(int id)
        {
            try
            {
                return await _db.Matches.Where(m => m.Id == id)
                    .Include(m => m.Group)
                    .Include(m => m.TeamMatches).SingleOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
