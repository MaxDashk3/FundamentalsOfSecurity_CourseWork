using Microsoft.AspNetCore.Identity;

namespace Movies.Models
{
    public class AppUser : IdentityUser
    {
        public IEnumerable<Ticket>? Tickets { get; set; }
        public IEnumerable<Purchase>? Purchases { get; set; }
    }
}
