using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using Movies.ViewModels;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(int? GenreId = null, int? MovieId = null)
        {
            if (GenreId != null)
            {
                var applicationDbContext = _context.Movies.
                 Where(x => x.GenreId == GenreId).Include(m => m.Genre)
                .Select(m => new MovieViewModel(m)).ToListAsync();
                return View(await applicationDbContext);
            }
            else if (MovieId != null)
            {
                var applicationDbContext = _context.Movies
                    .Where(x => x.Id == MovieId).Include(m => m.Genre)
                    .Select(m => new MovieViewModel(m)).ToListAsync();
                return View(await applicationDbContext);
            }
            else
            {
                var applicationDbContext = _context.Movies.Include(m => m.Genre)
                    .Select(m => new MovieViewModel(m)).ToListAsync();
                return View(await applicationDbContext);
            }
        }

        // GET: Movies/Details/5
        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Sessions.OrderBy(s => s.TimeDate))
                .ThenInclude(s => s.Hall)
                .FirstOrDefault(x => x.Id == id);
            if (movie == null)

                return RedirectToAction("Index");

            return View(new MovieViewModel(movie));
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewBag.Genres = _context.Genres.ToList();
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel model, IFormFile Poster)
        {
            model.Poster = FileToBytes(Poster);
            ModelState.Clear();
            if (TryValidateModel(model) && new DataController(_context).MoviesValidation(model.Title))
            {
                var movie = new Movie(model);
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Genres = _context.Genres.ToList();
            return View();
        }

        public byte[] FileToBytes(IFormFile file)
        {
            BinaryReader reader = new BinaryReader(file.OpenReadStream());
            byte [] imageBytes = reader.ReadBytes((int)file.Length);
            return imageBytes;

        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewBag.Genres = _context.Genres.ToList();
            return View(new MovieViewModel(movie));
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieViewModel model, IFormFile Poster)
        {
            var movie = new Movie(model);
            model.Poster = FileToBytes(Poster);
            ModelState.Clear();

            if (TryValidateModel(model) && new DataController(_context).MoviesValidation(model.Title))
            {
                if (id != movie.Id)
                {
                    return NotFound();
                }
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            ViewBag.Genres = _context.Genres.ToList();
            return View(new MovieViewModel(movie));
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(new MovieViewModel(movie));
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ShowImage(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                return File(movie.Poster, "image/jpeg"); // You can set the appropriate content type.
            }
            else
            {
                return Content("Image not found");
            }
        }


        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
