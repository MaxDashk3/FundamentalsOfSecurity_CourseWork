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

        [Authorize]
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                var session = _context.Sessions
                    .Include(s => s.Movie)
                    .Include(s => s.Hall)
                    .Include(s => s.Tickets)
                    .FirstOrDefault(s => s.Id == id);
                var sModel = new SessionViewModel(session!);
                var tickets = sModel.Tickets;
                var takenseats = tickets!.Select(t => new TicketViewModel(t))
                    .Select(t => t.Seat)
                    .ToList();
                ViewBag.Hall = session!.Hall;
                ViewBag.Session = sModel;
                ViewBag.TakenSeats = takenseats;
                ViewBag.UserId = _manager.GetUserId(User);
                return View();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(int SessionId, string UserId, string[] seat)
        {
            foreach (var s in seat)
            {
                var ticket = new Ticket()
                {
                     SeatRow = Convert.ToInt16(s.Remove(s.IndexOf(' '))),
                     SeatNum = Convert.ToInt16(s.Remove(0, s.IndexOf(' '))),
                     SessionId = SessionId,
                     UserId = UserId
                };
                _context.Tickets.Add(ticket);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Session");
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var ticket = await _context.Tickets.Include(t => t.Session)
                .FirstOrDefaultAsync(t => t.Id == id);
                return View(new TicketViewModel(ticket!));
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var ticket = await _context.Tickets.Include(t => t.Session.Movie)
                .FirstOrDefaultAsync(t => t.Id == id);
                return View(new TicketViewModel(ticket!));
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admins")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
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
                .FirstOrDefault(u => u.UserName == User.Identity!.Name)!
                .Tickets!
                .Where(t => t.PurchaseId == null)
                .Select(t => new TicketViewModel(t))
                .ToList();
            return View(tickets);
        }
    }
}