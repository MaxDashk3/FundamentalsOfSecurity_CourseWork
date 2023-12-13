using Movies.Models;

namespace Movies.ViewModels
{
    public class TicketViewModel
    {
        public TicketViewModel() { }
        public int Id { get; set; }
        public int? PurchaseId { get; set; }
        public int SessionId { get; set; }
        public string Seat { get; set; }
        public int SeatRow { get; set; }
        public int SeatNum { get; set; }
        public string UserId { get; set; }

        public string BoughtBy { get; set; }
        public DateTime DateOfPurchase { get; set; }

        public string Movie { get; set; }
        public int MovieId { get; set; }
        public DateTime SessionTime { get; set; }
        public string? Hall { get; set; }
        public int Price { get; set; }

        public TicketViewModel(Ticket ticket) 
        {
            Id = ticket.Id;
            PurchaseId = ticket.PurchaseId;
            SessionId = ticket.SessionId;
            Seat = ticket.SeatRow.ToString() + " " + ticket.SeatNum.ToString();
            SeatNum = ticket.SeatNum;
            SeatRow = ticket.SeatRow;
            UserId = ticket.UserId;

            if (ticket.User != null)
            {
                BoughtBy = ticket.User.UserName;
            }

            if (ticket.Purchase != null)
            {
                DateOfPurchase = ticket.Purchase.Date;
            }

            if (ticket.Session != null)
            {
                var SModel = new SessionViewModel(ticket.Session);
                if (SModel.Movie != null)
                    Movie = SModel.Movie;
                MovieId = SModel.MovieId;
                SessionTime = SModel.TimeDate;
                Hall = SModel.Hall;
                Price = SModel.Price;

            }
        }

    }
}
