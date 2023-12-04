using System.ComponentModel.DataAnnotations;
namespace Movies.Models
{
    public class Hall
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Number { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Range(5, 30, ErrorMessage ="There has to be from 5 to 30 rows")]
        public int Rows {  get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Range(5, 30, ErrorMessage = "There has to be from 5 to 30 seats per row")]
        public int SeatsPerRow { get; set; }

        public IEnumerable<Session>? Sessions { get; set; }
    }
}
