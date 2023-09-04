using GameLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameLibrary.DAL.EntityTypeConfiguration
{
    public class GameEntityConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder
                .HasKey(game => game.Id);
            builder
                .Property(game => game.Id)
                .ValueGeneratedOnAdd();
            builder
                .Property(game => game.Title)
                .IsRequired();
            builder
                .Property(game => game.DevelopmentStudio)
                .IsRequired();
        }
    }
}
