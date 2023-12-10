using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;

namespace Movies.Controllers
{
    public class DataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataController(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool GenresValidation(string Name)
        {
            var valid = !_context.Genres.Any(g => g.Name == Name);
            return valid;
        }
        public bool MoviesValidation(string Title)
        {
            bool valid = !_context.Movies.Any(m => m.Title == Title);
            return valid;
        }
    }
}
