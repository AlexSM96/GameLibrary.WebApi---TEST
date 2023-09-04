using GameLibrary.Domain.Entities.Base;

namespace GameLibrary.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public ICollection<Game> Games { get; set; } = new List<Game>();  
    }
}
