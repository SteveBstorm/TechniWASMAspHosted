using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WASMAspHosted.Server.Services;
using WASMAspHosted.Shared;

namespace WASMAspHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly FakeGameService _gs;

        public GameController(FakeGameService gs)
        {
            _gs = gs;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_gs.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_gs.GetById(id));
        }

        [HttpPost]
        public IActionResult Post(Game g)
        {
            _gs.Add(g);
            return Ok();
        }
    }
}
