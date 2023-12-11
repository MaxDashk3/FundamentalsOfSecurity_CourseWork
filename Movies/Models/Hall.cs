using Movies.ViewModels;
using System.ComponentModel.DataAnnotations;
namespace Movies.Models
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rows {  get; set; }
        public int SeatsPerRow { get; set; }

        public IEnumerable<Session>? Sessions { get; set; }
        public IEnumerable<Technology>? Technologies { get; set; }

        public Hall() { }

        public Hall(HallViewModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Rows = model.Rows;
            SeatsPerRow = model.SeatsPerRow;
        }
    }
}
