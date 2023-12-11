﻿using System;
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
            if (_context.Halls != null)
            {
                return View(await _context.Halls.Include(h => h.Technologies)
                    .Select(h => new HallViewModel(h))
                    .ToListAsync());
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Halls'  is null.");
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            var hall = await _context.Halls
                .Include(h => h.Technologies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hall == null)
            {
                return RedirectToAction("Index");
            }

            return View(new HallViewModel(hall));
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
            hall.Technologies = TechId.Select(t => _context.Technologies.Find(t)).ToList();

            if (ModelState.IsValid)
            {
                _context.Add(hall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Technologies = _context.Technologies.ToList();
            return View(hall);
        }

        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Edit(int? id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }
            ViewBag.Technologies = _context.Technologies.ToList();
            ViewBag.SelectedTech = _context.Halls.Include(h => h.Technologies)
                .FirstOrDefault(h => h.Id == id)
                .Technologies.Select(t => t.Id).ToList();
            return View(new HallViewModel(hall));
        }

        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HallViewModel model, int[] TechId)
        {
            var hall = new Hall(model);
            if (ModelState.IsValid)
            {
                if (id != hall.Id)
                {
                    return NotFound();
                }

                try
                {
                    ICollection<Technology> technologies = new HashSet<Technology>();
                    foreach (int i in TechId)
                    {
                        technologies.Add(_context.Technologies.Find(i));
                    }
                    var HallToUpd = _context.Halls.Include(h => h.Technologies)
                        .FirstOrDefault(h => h.Id == id);
                    HallToUpd.Name = hall.Name;
                    HallToUpd.Technologies = technologies;
                    _context.Update(HallToUpd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallExists(hall.Id))
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
            ViewBag.Technologies = _context.Technologies.ToList();
            return View(hall) ;
        }

        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            var hall = await _context.Halls
                .Include(h => h.Technologies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hall == null)
            {
                return RedirectToAction("Index");
            }

            return View(new HallViewModel(hall));
        }

        [Authorize(Roles = "Admins")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Halls == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Halls'  is null.");
            }
            var hall = await _context.Halls.FindAsync(id);
            if (hall != null)
            {
                _context.Halls.Remove(hall);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HallExists(int id)
        {
          return (_context.Halls?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
