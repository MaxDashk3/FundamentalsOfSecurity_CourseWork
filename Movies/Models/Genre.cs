using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Movies.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Remote(action: "Validation", controller: "Genres", ErrorMessage = "The genre already exists!")]
        public string Name { get; set; }

        public IEnumerable<Movie>? Movies { get; set;}
    }
}
