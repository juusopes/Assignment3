using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameWebApi.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IRepository _repository;

        [HttpGet("{i:int}")]
        public int Test(int i)
        {
            return i + i;
        }

        public PlayersController(ILogger<PlayersController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [Route("{playerId}")]
        public Task<Player> Get(Guid id)
        {
            return _repository.Get(id);
        }

        [HttpGet]
        [Route("getall")]
        public Task<Player[]> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpPost]
        [Route("create")]
        public Task<Player> Create([FromBody] NewPlayer player)
        {
            Player newPlayer = new Player() { Id = Guid.NewGuid(), Name = player.Name };

            return _repository.Create(newPlayer);
        }

        [HttpPost]
        [Route("delete")]
        public Task<Player> Delete([FromBody] Guid id)
        {
            return _repository.Delete(id);
        }
    }
}