using GameLibrary.Application.Interfaces;
using GameLibrary.DAL.EntityTypeConfiguration;
using GameLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.DAL
{
    public class GameLibraryDbContext : DbContext, IBaseDbContext
    {
        public GameLibraryDbContext(DbContextOptions<GameLibraryDbContext> option)
            : base(option) { }

        public DbSet<Game> Games { get; set; }

        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new GameEntityConfiguration())
                .ApplyConfiguration(new GenreEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
