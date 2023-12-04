using Movies.ViewModels;

namespace Movies.Models
{
    public class Purchase
    {
        public Purchase() { }
        public int PurchaseId { get; set; }
        public string Person { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<Ticket> Tickets { get; set; }

        public Purchase(PurchaseViewModel model)
        {
            PurchaseId = model.PurchaseId;
            Person = model.Person;
            Address = model.Address;
            Date = model.Date;
        }
    }
}
