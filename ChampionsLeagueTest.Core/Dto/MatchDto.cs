using System.Collections.Generic;
using ChampionsLeagueTest.DAL;
using Newtonsoft.Json;

namespace ChampionsLeagueTest.Core.Dto
{
    public class MatchDto
    {
        [JsonProperty("leagueTitle")]
        public string LeagueTitle { get; set; }

        [JsonProperty("matchday")]
        public int MatchDay { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("homeTeam")]
        public string HomeTeam { get; set; }

        [JsonProperty("awayTeam")]
        public string AwayTeam { get; set; }

        [JsonProperty("kickoffAt")]
        public string KickoffAt { get; set; }

        [JsonProperty("score")]
        public string Score { get; set; }

        public static List<MatchDto> ToMatchDto(List<Match> matches)
        {
            List<MatchDto> result = new List<MatchDto>();

            foreach (var match in matches)
            {
                var matchDto = new MatchDto()
                {
                    Group = match.Group.GroupName,
                    KickoffAt = match.KickOffAt.ToString(),
                    LeagueTitle = match.LeagueTitle,
                    Score = $"{match.GoalsHomeTeam}:{match.GoalsAwayTeam}",
                    MatchDay = match.MatchDay,
                    AwayTeam = match.AwayTeam,
                    HomeTeam = match.HomeTeam
                };

                result.Add(matchDto);
            }

            return result; 
        }
    }
}
