using Movies.ViewModels;

namespace Movies.Models
{
    public class Session
    {
        public Session() { }
        public int Id { get; set; }
        public int HallId { get; set; }
        public int MovieId { get; set; }
        public DateTime TimeDate { get; set; }
        public int Price { get; set; }

        public Hall Hall { get; set; }
        public Movie Movie { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }

        public Session(SessionViewModel model)
        {
            Id = model.Id;
            HallId = model.HallId;
            MovieId = model.MovieId;
            TimeDate = model.TimeDate;
            Price = model.Price;
        }
    }
}
