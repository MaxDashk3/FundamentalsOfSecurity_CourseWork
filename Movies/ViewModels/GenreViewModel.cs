using Movies.Models;
namespace Movies.ViewModels
{
    public class GenreViewModel
    {
        public int Id { get; set; }
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
