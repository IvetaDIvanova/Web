using System.ComponentModel.DataAnnotations;

namespace Web.Models.Books
{
    public class EditBookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The title is mandatory")]
        [StringLength(200, ErrorMessage = "Title can't be more than 200 symbols")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The author name is mandatory")]
        [StringLength(100, ErrorMessage = "Author name can't be more than 100 symbols")]
        public string Author { get; set; }

        [Required(ErrorMessage = "The year of publishing is mandatory")]
        public int Year { get; set; }

        [Required(ErrorMessage = "The the book's genre is mandatory")]
        public int GenreId { get; set; }
    }
}
