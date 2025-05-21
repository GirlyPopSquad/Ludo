using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollController : ControllerBase
    {
        private readonly IDiceService _diceService;
        private readonly IRollService _rollService;

        public RollController(IDiceService diceService, IRollService rollService)
        {
            _diceService = diceService;
            _rollService = rollService;
        }
        
        [HttpGet]
        public ActionResult<int> Get()
        {
            return _diceService.RollDice();
        }

        [HttpPost("isItA6/{roll}")]
        public ActionResult<bool> Isita6(int roll)
        {
            return _diceService.IsItA6(roll);
        }

        [HttpPost("NextRoll/{gameId}")]
        public ActionResult<Roll> DoNextRoll(int gameId)
        {
            return Ok(_rollService.DoNextRoll(gameId));
        } 
    }
}
