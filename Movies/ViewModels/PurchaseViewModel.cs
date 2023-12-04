using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.ViewModels
{
    public class PurchaseViewModel
    {
        public PurchaseViewModel() { }

        public int PurchaseId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Person { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }
        public DateTime Date { get; set; }

        public List<TicketViewModel>? Tickets { get; set; }

        public PurchaseViewModel(Purchase purchase)
        {
            PurchaseId = purchase.PurchaseId;
            Person = purchase.Person;
            Address = purchase.Address;
            Date = purchase.Date;

            if (purchase.Tickets != null) 
            {
                Tickets = purchase.Tickets.Select(t => new TicketViewModel(t)).ToList();
            }
        }
    }
}
