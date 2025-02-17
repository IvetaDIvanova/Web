using System.ComponentModel.DataAnnotations;

namespace Web.Models.Genres
{
    public class CreateGenreViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The genre name is mandatory")]
        [StringLength(200, ErrorMessage = "Genre name can't be more than 200 symbols")]
        public string Name { get; set; }
    }
}
