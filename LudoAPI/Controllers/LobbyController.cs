using LudoAPI.Models;
using LudoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private readonly ILobbyService _lobbyService;
        public LobbyController(ILobbyService lobbyService) 
        { 
            _lobbyService = lobbyService;
        }

        [HttpGet]
        public ActionResult<Lobby> CreateLobby()
        {
            var lobby = _lobbyService.CreateLobby();
            if (lobby == null)
            {
                return BadRequest("Lobby could not be created");
            }
            return Ok(lobby);
        }
    }
}
