using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.ViewModels
{
    public class GenreViewModel
    {
        public int Id { get; set; }
        [Remote(action: "GenresValidation", controller: "Data", ErrorMessage = "The genre already exists!")]
        [Required(ErrorMessage = "Enter")]
        public string Name { get; set; }
        public List<MovieViewModel>? Movies { get; set; }
        public GenreViewModel() { }
        public GenreViewModel(Genre genre) 
        { 
            Id = genre.Id;
            Name = genre.Name;
            if(genre.Movies != null)
            {
                Movies = genre.Movies.Select(x => new MovieViewModel(x)).ToList();
            }
        }
    }
}
