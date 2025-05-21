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

        [HttpPost("StartingRoll/{lobbyId}")]
        public ActionResult<Lobby> NextStartingRoll(int lobbyId)
        {
            return _startingService.DoNextStartingRoll(lobbyId);
        }
        
        [HttpGet("GetRerollers/{lobbyId}")]
        public ActionResult<List<Player>> GetReRollers(int lobbyId)
        {
            var rerollers = _startingService.GetReRollers(lobbyId);

            return Ok(rerollers);
        }

        [HttpPost("GetShouldReroll")]
        public ActionResult<bool> GetShouldReRoll([FromBody] List<Roll> startingRolls)
        {
            try
            {
                var shouldReroll = _startingService.ShouldReRoll(startingRolls);
                return Ok(shouldReroll);
            }
            catch
            {
                return BadRequest("Could not determine if reroll is needed");
            }
        }

        [HttpPost("HandleReroll/{lobbyId}")]
        public ActionResult<Lobby> HandleReroll(int lobbyId, [FromBody] int playerId)
        {
            return _startingService.HandleReroll(lobbyId, playerId);
        }
    }
}
