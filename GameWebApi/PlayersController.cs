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

        public void ConfigureServices()
        {

        }

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
        public async Task<Player> Create([FromBody] NewPlayer player)
        {
            DateTime localDate = DateTime.UtcNow;

            Player new_player = new Player();
            new_player.Name = player.Name;
            new_player.Id = Guid.NewGuid();
            new_player.Score = 0;
            new_player.Level = 0;
            new_player.IsBanned = false;
            new_player.CreationTime = localDate;

            await _repository.Create(new_player);
            return new_player;
        }

        [HttpPost]
        [Route("modify/{id:Guid}")]
        public async Task<Player> Modify(Guid id, [FromBody] ModifiedPlayer player)
        {
            await _repository.Modify(id, player);
            return null;
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<Player> Delete(Guid id)
        {
            await _repository.Delete(id);
            return null;
        }
    }
}