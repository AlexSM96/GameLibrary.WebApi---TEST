using GameLibrary.Domain.Entities.Base;

namespace GameLibrary.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string DevelopmentStudio { get; set; }

        public ICollection<Genre> Genres { get; set; } = new List<Genre>(); 
    }
}
