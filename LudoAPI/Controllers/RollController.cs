using LudoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollController : ControllerBase
    {
        private readonly IDiceService _diceService;

        public RollController(IDiceService diceService)
        {
            _diceService = diceService;
        }

        [HttpGet]
        public ActionResult<int> Get()
        {
            return _diceService.RollDice();
        }
    }
}
