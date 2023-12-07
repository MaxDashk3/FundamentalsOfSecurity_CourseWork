using Movies.ViewModels;

namespace Movies.Models
{
    public class Purchase
    {
        public Purchase() { }
        public int PurchaseId { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }

        public AppUser User { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }

        public Purchase(PurchaseViewModel model)
        {
            PurchaseId = model.PurchaseId;
            UserId = model.UserId;
            Date = model.Date;
        }
    }
}
