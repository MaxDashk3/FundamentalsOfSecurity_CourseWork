using System.ComponentModel.DataAnnotations;
namespace Movies.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        public IEnumerable<Movie>? Movies { get; set;}
    }
}
