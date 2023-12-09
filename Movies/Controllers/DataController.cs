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
        public ActionResult GenresValidation(string Name)
        {
            var valid = !_context.Genres.Any(g => g.Name == Name);
            return Json(valid);
        }
        public ActionResult FilmsValidation(string Title)
        {
            bool valid = !_context.Movies.Any(m => m.Title == Title);
            return Json(valid);
        }
    }
}
