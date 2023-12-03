using EnglishWebApp.ViewModels;

namespace EnglishWebApp.Models
{
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Word>? Words { get; set; }
        public IEnumerable<FillTheGapTask>? FillTasks { get; set; }

        public Theme() { }
        public Theme(ThemeViewModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
        }
    }
}
