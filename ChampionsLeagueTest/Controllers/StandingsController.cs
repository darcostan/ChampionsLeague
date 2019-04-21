using System.Threading.Tasks;
using ChampionsLeagueTest.Core.Interfaces;
using ChampionsLeagueTest.DAL;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsLeagueTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly IMatchService _service;

        public StandingsController(IMatchService service)
        {
            _service = service;
        }

        // GET api/standings
        [HttpGet]
        public async Task<ActionResult<GroupStanding>> Get()
        {
            var result = await _service.GetCurrentStandingsAsync();

            return result;
        }
    }
}