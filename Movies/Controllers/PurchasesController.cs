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
    public class PurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _manager;

        public PurchasesController(ApplicationDbContext context, UserManager<AppUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        // GET: Purchases
        public IActionResult Index()
        {
            var purchases = _context.Purchases
                .Include(p => p.Tickets)
                .Include("Tickets.Session")
                .Include("Tickets.Session.Movie")
                .Include("Tickets.Session.Hall")
                .Include("Tickets.User")
                .Select(p => new PurchaseViewModel(p))
                .ToList();

            return View(purchases);
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.Tickets)
                .Include("Tickets.Session.Movie")
                .Include("Tickets.Session.Hall")
                .Include("Tickets.User")
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(new PurchaseViewModel(purchase));
        }

        // GET: Purchases/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.UserId = _manager.GetUserId(User);
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(PurchaseViewModel model)
        {
            var purchase = new Purchase(model);
            purchase.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                List<Ticket> tickets = _context.Tickets
                .Where(t => t.UserId == _manager.GetUserId(User))
                .Where(t => t.PurchaseId == null)
                .ToList();

                _context.Purchases.Add(purchase);
                _context.SaveChanges();

                foreach (var t in tickets)
                {
                    t.PurchaseId = purchase.PurchaseId;
                    _context.Update(t);
                    _context.SaveChanges();
                }
                return RedirectToAction("BuyResult", new {Person = User.Identity.Name});
            }
            return View();
        }

        public IActionResult BuyResult(string Person)
        {
            if (Person != null)
            {
                ViewBag.Name = Person;
                return View();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewBag.UserId = _manager.GetUserId(User);
            return View(new PurchaseViewModel(purchase));
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseViewModel model)
        {
            var purchase = new Purchase(model);
            if (id != purchase.PurchaseId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(purchase);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(purchase.PurchaseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(new PurchaseViewModel(purchase));
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Purchases == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Purchases'  is null.");
            }
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase != null)
            {
                var ticketsToRemove = _context.Tickets
                    .Where(t => t.PurchaseId == purchase.PurchaseId).ToList();
                _context.Tickets.RemoveRange(ticketsToRemove);
                _context.Purchases.Remove(purchase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
          return (_context.Purchases?.Any(e => e.PurchaseId == id)).GetValueOrDefault();
        }
    }
}
