using GameLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Application.Interfaces
{
    public interface IBaseDbContext
    {
        public DbSet<Game> Games { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
