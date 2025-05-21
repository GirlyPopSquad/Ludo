using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollController : ControllerBase
    {
        private readonly IRollService _rollService;

        public RollController(IRollService rollService)
        {
            _rollService = rollService;
        }

        [HttpPost("NextRoll/{gameId}")]
        public ActionResult<Roll> DoNextRoll(int gameId)
        {
            return Ok(_rollService.DoNextRoll(gameId));
        } 
    }
}
