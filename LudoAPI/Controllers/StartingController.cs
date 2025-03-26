﻿using LudoAPI.Models;
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

        [HttpPost("GetRerollers")]
        public ActionResult<List<LobbyPlayer>> GetReRollers(List<Roll> startingRolls)
        {
            var rerollers = _startingService.GetReRollers(startingRolls);
            if (rerollers.Count == 0 || rerollers == null)
            {
                return BadRequest("Could not find rerollers");
            }

            return Ok(rerollers);
        }

        [HttpGet("GetShouldReroll")]
        public ActionResult<bool> GetShouldReRoll(List<Roll> startingRolls)
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
    }
}
