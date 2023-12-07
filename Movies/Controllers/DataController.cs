using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    public class DataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataController(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CheckGenres(string Name)
        {
            var DoesGenreExist = _context.Genres.Where(g => g.Name == Name).Any();
            return !DoesGenreExist;
        }
        public bool CheckMovies(string Title)
        {
            var DoesGenreExist = _context.Movies.Where(m => m.Title == Title).Any();
            return !DoesGenreExist;
        }
    }
}
