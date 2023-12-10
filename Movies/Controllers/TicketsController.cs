using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using Movies.ViewModels;

namespace Movies.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _manager;

        public TicketsController(ApplicationDbContext context, UserManager<AppUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        [Authorize(Roles = "Admins")]
        public IActionResult Create(int id)
        {
            var session = _context.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Include(s => s.Tickets)
                .FirstOrDefault(s => s.Id == id);

            if (session != null)
            {
                var sModel = new SessionViewModel(session);
                var tickets = sModel.Tickets;
                var takenseats = tickets.Select(t => new TicketViewModel(t))
                    .Select(t => t.Seat)
                    .ToList();

                ViewBag.Hall = session.Hall;
                ViewBag.Session = sModel;
                ViewBag.TakenSeats = takenseats;
                ViewBag.UserId = _manager.GetUserId(User);
                return View();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Admins")]
        public IActionResult Create(TicketViewModel model)
        {
            model.SeatRow = Convert.ToInt16(model.Seat.Remove(model.Seat.IndexOf(' ')));
            model.SeatNum = Convert.ToInt16(model.Seat.Remove(0 , model.Seat.IndexOf(' ')));
            var ticket = new Ticket(model);

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return RedirectToAction("Index","Session");
        }

        public IActionResult Index()
        {
            var tickets = _context.Tickets
                .Include(t => t.Session)
                .Include(t => t.Session.Movie)
                .Include(t => t.Session.Hall)
                .Include(t => t.Session.Movie.Genre)
                .Include(t => t.Purchase)
                .Include(t => t.User)
                .Select(t => new TicketViewModel(t))
                .ToList();

            return View(tickets);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Session)
                .ThenInclude(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            var model = new TicketViewModel(ticket);

            return View(model);
        }

        [Authorize(Roles = "Admins")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tickets'  is null.");
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult Cart()
        {
            var tickets = _context.Users
                .Include(u => u.Tickets)
                .Include("Tickets.Session")
                .Include("Tickets.Session.Movie")
                .Include("Tickets.Session.Hall")
                .FirstOrDefault(u => u.UserName == User.Identity.Name)
                .Tickets
                .Where(t => t.PurchaseId == null)
                .Select(t => new TicketViewModel(t))
                .ToList();

            return View(tickets);
        }

        private bool TicketExists(int id)
        {
          return (_context.Tickets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
