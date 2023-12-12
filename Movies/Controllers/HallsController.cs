using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using Movies.ViewModels;

namespace Movies.Controllers
{
    public class HallsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HallsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Halls.Include(h => h.Technologies)
                    .Select(h => new HallViewModel(h))
                    .ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var hall = await _context.Halls
                .Include(h => h.Technologies)
                .FirstOrDefaultAsync(m => m.Id == id);
                if (hall != null)
                {
                    return View(new HallViewModel(hall));
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admins")]
        public IActionResult Create()
        {
            ViewBag.Technologies = _context.Technologies.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Create(HallViewModel model, int[] TechId)
        {
            var hall = new Hall(model);
            hall.Technologies = TechId.Select(t => _context.Technologies.Find(t)).ToList()!;
            if (ModelState.IsValid && new DataController(_context).HallsValidation(hall.Name))
            {
                _context.Add(hall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Technologies = await _context.Technologies.ToListAsync();
            return View(model);
        }

        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var hall = await _context.Halls.FindAsync(id);
                if (hall != null)
                {
                    ViewBag.Technologies = _context.Technologies.ToList();
                    var selectedTech = _context.Halls.Include(h => h.Technologies)
                        .FirstOrDefault(h => h.Id == id);
                    if (selectedTech != null) { 
                        ViewBag.SelectedTech = selectedTech.Technologies?.Select(t => t.Id).ToList();
                    }
                    return View(new HallViewModel(hall));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HallViewModel model, int[] TechId)
        {
            var hall = new Hall(model);
            if (ModelState.IsValid && new DataController(_context).HallsValidation(hall.Name, hall.Id))
            {
                if (id != hall.Id)
                {
                    return NotFound();
                }
                ICollection<Technology> technologies = new HashSet<Technology>();
                foreach (int i in TechId)
                {
                    var technology = _context.Technologies.Find(i);
                    if (technology != null)
                    {
                        technologies.Add(technology);
                    }
                }
                var HallToUpd = _context.Halls.Include(h => h.Technologies)
                    .FirstOrDefault(h => h.Id == id);
                if (HallToUpd != null)
                {
                    HallToUpd.Name = hall.Name;
                    HallToUpd.Rows = hall.Rows;
                    HallToUpd.SeatsPerRow = hall.SeatsPerRow;
                    HallToUpd.Technologies = technologies;
                    _context.Update(HallToUpd);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Technologies = _context.Technologies.ToList();
            return View(hall) ;
        }

        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var hall = await _context.Halls
                    .Include(h => h.Technologies)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (hall != null)
                {
                    return View(new HallViewModel(hall));
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admins")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall != null)
            {
                _context.Halls.Remove(hall);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
