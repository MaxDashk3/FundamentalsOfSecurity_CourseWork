using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Movies.ViewModels;
namespace Movies.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Movie>? Movies { get; set; }
        public Genre() { }
        public Genre(GenreViewModel genre)
        {
            Id = genre.Id;
            Name = genre.Name;
        }
    }
}
