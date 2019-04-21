using System.Collections.Generic;
using System.Threading.Tasks;
using ChampionsLeagueTest.Core.Dto;
using ChampionsLeagueTest.Core.Interfaces;
using ChampionsLeagueTest.DAL;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _service;

        public MatchesController(IMatchService service)
        {
            _service = service;
        }

        // GET api/matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDto>>> Get()
        {
            var result = MatchDto.ToMatchDto(await _service.ListMatches(null, null, null, null));

            return result;
        }

        // GET api/matches/fromDate/toDate/groupName/teamName
        [HttpGet("{dateTime}/{groupName}/{team}")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> Get(string fromDate, string toDate, string groupName, string team)
        {
            var result = MatchDto.ToMatchDto(await _service.ListMatches(fromDate, toDate, groupName, team));

            return result;
        }

        // GET api/matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> Get(int id)
        {
            return await _service.ListMatches(id);
        }

        // POST api/matches
        [HttpPost]
        public async Task<ActionResult<List<ResponseDto>>> Post([FromBody] List<MatchDto> data)
        {
            var standings = await _service.LoadMatchesAsync(data);

            var result = ResponseDto.ToResponseDto(standings);

            return result;
        }
    }
}
