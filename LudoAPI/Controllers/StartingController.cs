using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public ActionResult<Lobby> GetStartingRoll(Lobby lobby)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult<List<LobbyPlayer>> GetReRollers(List<Roll> startingRolls)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult<bool> GetShouldReRoll(List<Roll> startingRolls)
        {
            throw new NotImplementedException();
        }
    }
}
