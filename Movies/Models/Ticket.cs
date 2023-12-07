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
        public string UserId { get; set; }


        public AppUser User { get; set; }
        public Purchase? Purchase { get; set; }
        public Session Session { get; set; }

        public Ticket(TicketViewModel model)
        {
            Id = model.Id;
            PurchaseId = model.PurchaseId;
            SessionId = model.SessionId;
            SeatRow = model.SeatRow;
            SeatNum = model.SeatNum;
            UserId = model.UserId;
        }
    }
}
