using GameLibrary.Domain.Entities;

namespace GameLibrary.Application.Services.Base
{
    public interface IGameService
    {
        public Task<IEnumerable<Game>> GetGames(CancellationToken cancellationToken);

        public Task<IEnumerable<Game>> GetGames(string genre, CancellationToken cancellationToken);

        public Task CreateAsync(Game entity, CancellationToken cancellationToken);

        public Task UpdateAsync(Game entity, CancellationToken cancellationToken);

        public Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
