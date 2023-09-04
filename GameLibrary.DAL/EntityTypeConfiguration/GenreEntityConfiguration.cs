using GameLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameLibrary.DAL.EntityTypeConfiguration
{
    public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder
                .HasKey(genre => genre.Id);
            builder
                .Property(genre => genre.Title)
                .IsRequired();
            builder
                .HasMany(genre => genre.Games)
                .WithMany(g => g.Genres)
                .UsingEntity(x=>x.ToTable("GameGenres"));
        }
    }
}
