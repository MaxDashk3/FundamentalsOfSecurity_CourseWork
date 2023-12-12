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
using Movies.ViewModels;
using Movies.Controllers;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
             var applicationDbContext = _context.Movies
                .Include(m => m.Genre)
                .Select(m => new MovieViewModel(m)).ToListAsync();
                return View(await applicationDbContext);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var movie = await _context.Movies
                    .Include(m => m.Genre)
                    .Include(m => m.Sessions!.OrderBy(s => s.TimeDate))
                    .ThenInclude(s => s.Hall)
                    .FirstOrDefaultAsync(x => x.Id == id);
                return View(new MovieViewModel(movie!));
            }
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            ViewBag.Genres = _context.Genres.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admins")]
        public async Task<IActionResult> Create(MovieViewModel model, IFormFile Poster)
        {
            bool remote = new DataController(_context).MoviesValidation(model.Title);
            if (Poster != null)
            {
                model.Poster = FileToBytes(Poster);
                ModelState.Clear();
                if (ModelState.IsValid)
                {
                    var movie = new Movie(model);
                    _context.Add(movie);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            if (!remote)
            {
                ModelState.AddModelError("Title", "Title of film is already exists!");
            }
            ViewBag.Genres = _context.Genres.ToList();
            return View(model);
        }

        public byte[] FileToBytes(IFormFile file)
        {
            BinaryReader reader = new BinaryReader(file.OpenReadStream());
            byte[] imageBytes = reader.ReadBytes((int)file.Length);
            return imageBytes;

        }

        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var movie = await _context.Movies
                .FindAsync(id);
                ViewBag.Genres = _context.Genres.ToList();
                return View(new MovieViewModel(movie!));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Edit(int id, MovieViewModel model, IFormFile NewPoster)
        {
            var movie = new Movie(model);
            if (NewPoster != null)
                movie.Poster = FileToBytes(NewPoster);
            else {
                var moviefind = _context.Movies.Find(movie.Id);
                if (moviefind != null) movie.Poster = moviefind.Poster;
            }
            ModelState.Clear();
            bool remote = new DataController(_context).MoviesValidation(movie.Title, movie.Id);
            if (ModelState.IsValid && remote)
            {
                if (id != movie.Id)
                {
                    return NotFound();
                }
                _context.ChangeTracker.Clear();
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!remote)
            {
                ModelState.AddModelError("Title", "Title of film is already exists!");
            }
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            return View(new MovieViewModel(movie));
        }

        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var movie = await _context.Movies
                .FindAsync(id);
                return View(new MovieViewModel(movie!));
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admins")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ShowImage(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                return File(movie.Poster, "image/jpeg"); 
            }
            else
            {
                return Content("Image not found");
            }
        }
    }
}

