using System.ComponentModel.DataAnnotations;

namespace GameLibrary.Domain.Dto
{
    public class GameDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название игры")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите название студии разработчиков")]
        public string DevelopmentStudio { get; set; }

        public List<GenreDto> Genres { get; set; }
    }
}
