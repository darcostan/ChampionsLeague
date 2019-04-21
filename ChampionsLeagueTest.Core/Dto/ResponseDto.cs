using System.Collections.Generic;
using ChampionsLeagueTest.DAL;
using Newtonsoft.Json;

namespace ChampionsLeagueTest.Core.Dto
{
    public class ResponseDto
    {
        [JsonProperty("leagueTitle")]
        public string LeagueTitle { get; set; }

        [JsonProperty("matchday")]
        public long Matchday { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("standing")]
        public List<TeamStandingDto> Standing { get; set; }

        public static List<ResponseDto> ToResponseDto(List<GroupStanding> groupStandings)
        {
            var result = new List<ResponseDto>();

            foreach (var res in groupStandings)
            {
                var response = new ResponseDto
                {
                    Group = res.Group.GroupName,
                    LeagueTitle = "",
                    Matchday = res.MachDay,
                    Standing = new List<TeamStandingDto>(res.TeamStandings.Count)
                };

                foreach (var teamStanding in res.TeamStandings)
                {
                    response.Standing.Add(new TeamStandingDto()
                    {
                        Team = teamStanding.Team.Name,
                        Draw = teamStanding.Draw,
                        Points = teamStanding.Points,
                        GoalDifference = teamStanding.GoalDifference,
                        PlayedGames = teamStanding.PlayedGames,
                        Win = teamStanding.Win,
                        Lose = teamStanding.Lose,
                        Goals = teamStanding.Goals,
                        GoalsAgainst = teamStanding.GoalsAgainst,
                        Rank = teamStanding.Rank
                    });
                }
                result.Add(response);
            }

            return result;
        }
    }

    public class TeamStandingDto
    {
        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }

        [JsonProperty("playedGames")]
        public long PlayedGames { get; set; }

        [JsonProperty("points")]
        public long Points { get; set; }

        [JsonProperty("goals")]
        public long Goals { get; set; }

        [JsonProperty("goalsAgainst")]
        public long GoalsAgainst { get; set; }

        [JsonProperty("goalDifference")]
        public long GoalDifference { get; set; }

        [JsonProperty("win")]
        public long Win { get; set; }

        [JsonProperty("lose")]
        public long Lose { get; set; }

        [JsonProperty("draw")]
        public long Draw { get; set; }
    }
}