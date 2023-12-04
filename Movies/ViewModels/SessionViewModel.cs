using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.ViewModels
{
    public class SessionViewModel
    {
        public SessionViewModel() { }

        public int Id { get; set; }
        public int HallId { get; set; }
        public int? Hall { get; set; }
        public string? Movie { get; set; }
        public int MovieId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime TimeDate { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Range(40, 500, ErrorMessage = "Price has to be between 40 and 500")]
        public int Price { get; set; }

        public List<Ticket>? Tickets { get; set; }

        public SessionViewModel(Session session)
        {
            Id = session.Id;
            HallId = session.HallId;
            MovieId = session.MovieId;
            TimeDate = session.TimeDate;
            Price = session.Price;

            if (session.Tickets != null) 
            {
                Tickets = session.Tickets.ToList();
            }
            if (session.Hall != null)
            {
                Hall = session.Hall.Number;
            }
            if (session.Movie != null)
            {
                Movie = session.Movie.Title;
            }
        }
    }
}
