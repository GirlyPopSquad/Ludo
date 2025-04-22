using LudoAPI.Models;
using LudoAPI.Services;
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

        [HttpPost]
        public ActionResult<Lobby> Create()
        {
            var lobby = _lobbyService.CreateLobby();
            if (lobby == null)
            {
                return BadRequest("Lobby could not be created");
            }
            return Ok(lobby);
        }

        [HttpGet("{id}")]
        public ActionResult<Lobby> Get(int id)
        {
            var lobby = _lobbyService.GetLobbyById(id);
            if (lobby == null)
            {
                return BadRequest("Lobby could not be created");
            }
            return Ok(lobby);
        }
    }
}
