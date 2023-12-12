using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using Movies.ViewModels;
namespace Movies.Controllers
{
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Genres.Select(x => new GenreViewModel(x)).ToListAsync());
        }
        [Authorize(Roles = "Admins")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Create(GenreViewModel genreView)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Genre(genreView));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genreView);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var genre = await _context.Genres.Include(g => g.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);
                if (genre != null)
                {
                    return View(new GenreViewModel(genre));
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles ="Admins")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var genre = await _context.Genres
                .FindAsync(id);
                if(genre != null)
                return View(new GenreViewModel(genre));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admins")]
        public async Task<IActionResult> Edit(GenreViewModel genreView)
        {
            if (ModelState.IsValid)
            {
                 _context.Update(new Genre(genreView));
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
            }
            return View(genreView);
        }
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var genre = await _context.Genres
                .FindAsync(id);
                if(genre != null)
                return View(new GenreViewModel(genre));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admins")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Remove(genre);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
