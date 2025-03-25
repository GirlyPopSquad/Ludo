using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartingController : ControllerBase
    {
        private readonly IStartingService _startingService;
        public StartingController(IStartingService startingService)
        {
            _startingService = startingService;
        }

        [HttpPost("StartingRoll")]
        public ActionResult<Lobby> GetStartingRoll([FromBody] Lobby lobby)
        {
            return _startingService.StartingRoll(lobby);
        }

        [HttpGet("GetRerollers")]
        public ActionResult<List<LobbyPlayer>> GetReRollers(List<Roll> startingRolls)
        {
            throw new NotImplementedException();
        }

        [HttpGet("GetShouldReroll")]
        public ActionResult<bool> GetShouldReRoll(List<Roll> startingRolls)
        {
            throw new NotImplementedException();
        }
    }
}
