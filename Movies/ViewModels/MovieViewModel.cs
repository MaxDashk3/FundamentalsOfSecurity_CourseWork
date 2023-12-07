using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.ViewModels
{
    public class MovieViewModel
    {
        public MovieViewModel() { }

        public int Id { get; set; }
        public string? Genre { get; set; }

        [Remote(action: "CheckMovies", controller: "Data", ErrorMessage = "Title already exists!!!")]
        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Country { get; set; }
        public int GenreId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(1900, 2024, ErrorMessage = "The value has to be between 1900 and 2024")]
        public int Year { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string PhotoPath { get; set; }
        [Required]
        public string VideoPath { get; set; }

        public List<SessionViewModel>? Sessions { get; set; }

        public MovieViewModel(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            Country = movie.Country;
            Year = movie.Year;
            GenreId = movie.GenreId;
            Description = movie.Description;
            PhotoPath = movie.PhotoPath;
            VideoPath = movie.VideoPath;

            if (movie.Sessions != null)
            {
                Sessions = movie.Sessions.Select(s => new SessionViewModel(s)).ToList();
            }

            if (movie.Genre != null)
            {
                Genre = movie.Genre.Name;
            }
        }
    }
}
