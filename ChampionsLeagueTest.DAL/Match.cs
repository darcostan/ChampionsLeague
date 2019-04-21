using System;
using System.Collections.Generic;
using ChampionsLeagueTest.DAL.Enum;

namespace ChampionsLeagueTest.DAL
{
    public class Match
    {
        public int Id { get; set; }
        public string LeagueTitle { get; set; }
        public int MatchDay { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime KickOffAt { get; set; }
        public int GoalsHomeTeam { get; set; }
        public int GoalsAwayTeam { get; set; }
        public string Score => $"{GoalsHomeTeam}:{GoalsAwayTeam}";
        public virtual ICollection<TeamMatch> TeamMatches { get; set; }

        public static (int homeGoals, int awayGoals) GetResult(string score)
        {
            var parseString = score.Split(':');

            if (!int.TryParse(parseString[0], out var home))
            {
                return (-1, 0);
            }

            if (!int.TryParse(parseString[1], out var away))
            {
                return (0, -1);
            }

            return (home, away);
        }

        public static Result GetWinner(int goals, int goalsAgainst)
        {
            if (goals > goalsAgainst)
            {
                return Result.Win;
            }

            if (goalsAgainst > goals)
            {
                return Result.Lose;
            }

            return Result.Draw;
        }
    }
}
