using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    public class TechnologiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TechnologiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Technologies.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var technology = await _context.Technologies
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (technology != null)
                {
                    return View(technology);
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admins")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Technology technology)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technology);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technology);
        }
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var technology = await _context.Technologies.FindAsync(id);
                if (technology != null)
                {
                    return View(technology);
                }
            }
            return RedirectToAction(nameof(Index)); 
        }
        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Technology technology)
        {
            if (ModelState.IsValid)
            {
                if (id == technology.Id)
                {
                    _context.Update(technology);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(technology);
        }
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var technology = await _context.Technologies
                .FirstOrDefaultAsync(m => m.Id == id);
                if (technology == null)
                {
                    return View(technology);
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admins")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technology = await _context.Technologies.FindAsync(id);
            if (technology != null)
            {
                _context.Technologies.Remove(technology);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
