using EnglishWebApp.ViewModels;

namespace EnglishWebApp.Models
{
    public class FillTheGapTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public int? ThemeId { get; set; }

        public Theme? Theme { get; set; }
        public IEnumerable<Word> Words { get; set; }

        public FillTheGapTask() { }
        public FillTheGapTask(FillTheGapTaskViewModel model)
        {
            Id = model.Id;
            Text = model.Text;
            Name = model.Name;
            ThemeId = model.ThemeId;
        }
    }
}
