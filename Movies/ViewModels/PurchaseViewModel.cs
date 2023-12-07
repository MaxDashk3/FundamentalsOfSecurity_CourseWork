using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.ViewModels
{
    public class PurchaseViewModel
    {
        public PurchaseViewModel() { }

        public int PurchaseId { get; set; }
        public string UserId { get; set; } 
        public DateTime Date { get; set; }

        public string? UserName { get; set; }

        public List<TicketViewModel>? Tickets { get; set; }

        public PurchaseViewModel(Purchase purchase)
        {
            PurchaseId = purchase.PurchaseId;
            Date = purchase.Date;

            if (purchase.Tickets != null) 
            {
                Tickets = purchase.Tickets.Select(t => new TicketViewModel(t)).ToList();
            }
            if (purchase.User != null)
            {
                UserName = purchase.User.UserName;
            }
        }
    }
}
