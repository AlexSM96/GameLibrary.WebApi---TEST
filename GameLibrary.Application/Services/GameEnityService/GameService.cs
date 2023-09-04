using GameLibrary.Application.Exceptions;
using GameLibrary.Application.Interfaces;
using GameLibrary.Application.Services.Base;
using GameLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Application.Services.GameEnityService
{
    public class GameService : IGameService
    {
        private readonly IBaseDbContext _context;

        public GameService(IBaseDbContext context) => _context = context;

        public async Task<IEnumerable<Game>> GetGames(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Games
                    .Include(game => game.Genres)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Game>> GetGames(string genre, CancellationToken cancellationToken)
        {
            try
            {
                var games = await _context.Games
                    .Include(x => x.Genres)
                    .SelectMany(game => game.Genres,
                        (game, genre) => new { Game = game, Genre = genre })
                    .GroupBy(x => x.Genre.Title, x => x.Game)
                    .ToDictionaryAsync(x => x.Key, x => x.ToList(),
                        StringComparer.OrdinalIgnoreCase);

                if (!games.ContainsKey(genre))
                {
                    throw new NotFoundException(nameof(games), genre);
                }

                return games[genre];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateAsync(Game game, CancellationToken cancellationToken)
        {
            try
            {
                var newGame = await _context.Games
                    .FirstOrDefaultAsync(gameFromDb => gameFromDb.Title == game.Title
                        && gameFromDb.DevelopmentStudio == game.DevelopmentStudio);

                if (newGame is not null)
                {
                    throw new IsAlreadyExistException(nameof(Game));
                }

                newGame = await AddOrUpdateGame(game);
                await _context.Games.AddAsync(newGame, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Game game, CancellationToken cancellationToken)
        {
            try
            {
                var gameFromDb = await _context.Games
                    .Include(g => g.Genres)
                    .FirstOrDefaultAsync(g => g.Id == game.Id);

                if (gameFromDb is null)
                {
                    throw new NotFoundException(nameof(Game), game.Id);
                }

                gameFromDb = await AddOrUpdateGame(game, gameFromDb);

                _context.Games.Update(gameFromDb);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var game = await _context.Games
                    .FirstOrDefaultAsync(g => g.Id == id);

                if (game is null)
                {
                    throw new NotFoundException(nameof(Game), id);
                }

                _context.Games.Remove(game);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<Game> AddOrUpdateGame(Game gameFromView, Game gameFromDb = null!)
        {
            var newGame = gameFromDb is null ? new Game() : gameFromDb;
            foreach (var newGenre in gameFromView.Genres)
            {
                var genreFromDb = await _context.Genres
                    .FirstOrDefaultAsync(g => g.Title == newGenre.Title);
                AddGenreToGame(newGame, newGenre, genreFromDb);
            }

            newGame.Title = gameFromView.Title;
            newGame.DevelopmentStudio = gameFromView.DevelopmentStudio;
            return newGame;
        }

        private void AddGenreToGame(Game gameFromDb, Genre newGenre, Genre? genreFromDb)
        {
            if (genreFromDb is null)
            {
                gameFromDb.Genres.Add(newGenre);
                return;
            }

            gameFromDb.Genres.Add(genreFromDb);
        }

    }
}
