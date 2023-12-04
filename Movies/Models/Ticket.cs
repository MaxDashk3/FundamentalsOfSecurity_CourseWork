using Movies.ViewModels;

namespace Movies.Models
{
    public class Ticket
    {
        public Ticket() { }
        public int Id { get; set; }
        public int? PurchaseId { get; set; }
        public int SessionId { get; set; }
        public int SeatRow { get; set; }
        public int SeatNum { get; set; }

        public Purchase? Purchase { get; set; }
        public Session Session { get; set; }

        public Ticket(TicketViewModel model)
        {
            Id = model.Id;
            PurchaseId = model.PurchaseId;
            SessionId = model.SessionId;
            SeatRow = Convert.ToInt16(model.Seat.Remove(model.Seat.IndexOf(" ")));
            SeatNum = Convert.ToInt16(model.Seat.Remove(0, model.Seat.IndexOf(" ")));
        }
    }
}
