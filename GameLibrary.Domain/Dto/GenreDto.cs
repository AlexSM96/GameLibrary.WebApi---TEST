using System.ComponentModel.DataAnnotations;

namespace GameLibrary.Domain.Dto
{
    public class GenreDto
    {
        [Required(ErrorMessage = "Ввдеите название жанра")]
        public string Title { get; set; }
    }
}
