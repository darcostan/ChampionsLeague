using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChampionsLeagueTest.Core.Interfaces;
using ChampionsLeagueTest.DAL;
using ChampionsLeagueTest.DAL.Enum;
using Microsoft.EntityFrameworkCore;

namespace ChampionsLeagueTest.Core.BusinessLogic
{
    public class StandingService : IStandingService
    {
        private readonly ChampionsLeagueTestDbContext _db;

        public StandingService(ChampionsLeagueTestDbContext db)
        {
            _db = db;
        }

        public async Task<GroupStanding> CurrentStandings(Match match)
        {
            var groupStandings =
                await _db.GroupStandings.SingleOrDefaultAsync(s => s.GroupId == match.GroupId && s.MachDay == match.MatchDay);

            if (groupStandings is null)
            {
                groupStandings = new GroupStanding { GroupId = match.GroupId, Group = match.Group, MachDay = match.MatchDay };

                _db.GroupStandings.Add(groupStandings);
                await _db.SaveChangesAsync();
            }

            var homeStanding = await StandingsTeam(groupStandings, match.HomeTeam, match.GoalsHomeTeam, match.GoalsAwayTeam);

            if (homeStanding is null)
            {
                return null;
            }

            var awayStanding = await StandingsTeam(groupStandings, match.AwayTeam, match.GoalsAwayTeam, match.GoalsHomeTeam);

            if (awayStanding is null)
            {
                return null;
            }

            return _db.GroupStandings.SingleOrDefault(s => s.GroupId == match.GroupId && s.MachDay == match.MatchDay);
        }

        private async Task<TeamStanding> StandingsTeam(GroupStanding groupStanding, string teamName, int goals, int goalsAgainst)
        {
            var lastGroupStanding = await _db.GroupStandings.Where(g => g.GroupId == groupStanding.GroupId && g.MachDay != groupStanding.MachDay)
                .Include(g => g.TeamStandings)
                .Include(g => g.Group)
                .OrderByDescending(g => g.MachDay)
                .FirstOrDefaultAsync();

            TeamStanding teamStanding = null;

            var team = await _db.Teams.SingleOrDefaultAsync(t => t.Name == teamName);

            if (lastGroupStanding?.TeamStandings != null && lastGroupStanding.TeamStandings.Any())
            {
                teamStanding = lastGroupStanding.TeamStandings.SingleOrDefault(t => t.TeamId == team.Id);
            }

            var currentTeamStandings = new TeamStanding
            {
                TeamId = team.Id,
                Team = team,
                GroupStandingId = groupStanding.Id
            };

            if (teamStanding is null)
            {
                currentTeamStandings.PlayedGames ++;
                currentTeamStandings.Goals = goals;
                currentTeamStandings.GoalsAgainst = goalsAgainst;

                var result = Match.GetWinner(goals, goalsAgainst);
                if (result == Result.Win)
                {
                    currentTeamStandings.Points += 3;
                    currentTeamStandings.Win++;
                }
                else if (result == Result.Lose)
                {
                    currentTeamStandings.Lose++;
                }
                else
                {
                    currentTeamStandings.Draw++;
                    currentTeamStandings.Points += 1;
                }
            }
            else
            {
                currentTeamStandings.PlayedGames = teamStanding.PlayedGames + 1;
                currentTeamStandings.Goals = teamStanding.Goals + goals;
                currentTeamStandings.GoalsAgainst = teamStanding.GoalsAgainst + goalsAgainst;
                
                var result = Match.GetWinner(goals, goalsAgainst);
                if (result == Result.Win)
                {
                    currentTeamStandings.Points = teamStanding.Points + 3;
                    currentTeamStandings.Win = teamStanding.Win +1 ;
                    currentTeamStandings.Draw = teamStanding.Draw;
                    currentTeamStandings.Lose = teamStanding.Lose;
                }
                else if (result == Result.Lose)
                {
                    currentTeamStandings.Lose = teamStanding.Lose +1;
                    currentTeamStandings.Win = teamStanding.Win;
                    currentTeamStandings.Draw = teamStanding.Draw;
                    currentTeamStandings.Points = teamStanding.Points;
                }
                else
                {
                    currentTeamStandings.Draw = teamStanding.Draw +1;
                    currentTeamStandings.Points = teamStanding.Points + 1;
                    currentTeamStandings.Lose = teamStanding.Lose;
                    currentTeamStandings.Win = teamStanding.Win;
                }
            }

            _db.TeamStandings.Add(currentTeamStandings);
            await _db.SaveChangesAsync();

            currentTeamStandings.Rank = Ranking(groupStanding.TeamStandings, team);

            return currentTeamStandings;
        }

        private int Ranking(List<TeamStanding> teamStandings, Team team)
        {

            if (teamStandings.Count == 1)
            {
                return 1;
            }

            var list = teamStandings.OrderByDescending(t => t.Points).ThenByDescending(t => t.Goals).ThenByDescending(t => t.GoalDifference).ToList();

            foreach (var t in teamStandings)
            {
                t.Rank = list.IndexOf(t) + 1;
            }

            _db.SaveChanges();

            var result = teamStandings.SingleOrDefault(t => t.TeamId == team.Id);

            return result?.Rank ?? 0;
        }
    }
}
