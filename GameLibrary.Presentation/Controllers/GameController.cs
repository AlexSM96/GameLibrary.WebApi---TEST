using AutoMapper;
using GameLibrary.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Presentation.Controllers
{
    [ApiController]
    [Route($"api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        public GameController(IGameService service, IMapper mapper)
            => (_service,_mapper) = (service, mapper);

        [HttpGet("GetGames")]
        public async Task<IEnumerable<GameDto>> GetAllGames()
        {
            var games = await _service.GetGames(CancellationToken.None);

            if (!games.Any())
            {
                return Enumerable.Empty<GameDto>(); 
            }

            return _mapper.Map<List<GameDto>>(games);
        }

        [HttpGet("GetGames/{genre}")]
        public async Task<IEnumerable<GameDto>> GetGamesByGenre(string genre)
        {
            var games = await _service.GetGames(genre,CancellationToken.None);

            if (!games.Any())
            {
                return Enumerable.Empty<GameDto>();
            }

            return _mapper.Map<List<GameDto>>(games); 
        }

        [HttpPost("AddGame")]
        public async Task<IActionResult> AddGame([FromBody] GameDto model)
        {
            if (ModelState.IsValid)
            {
                var game = _mapper.Map<Game>(model);
                await _service.CreateAsync(game, CancellationToken.None);
                return Ok(model);
            }

            return BadRequest(model);
        }

        [HttpPut("UpdateGame")]
        public async Task<IActionResult> UpdateGame([FromBody] GameDto model)
        {
            if (ModelState.IsValid)
            {
                var game = _mapper.Map<Game>(model);
                await _service.UpdateAsync(game, CancellationToken.None);
                return Ok(model);
            }

            return BadRequest(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await _service.DeleteAsync(id, CancellationToken.None);
            return Ok(new {Message = "Deleted successfully"});
        }

    }
}
