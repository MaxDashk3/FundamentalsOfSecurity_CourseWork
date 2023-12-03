using EnglishWebApp.Models;

namespace EnglishWebApp.ViewModels
{
    public class ThemeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<WordViewModel>? Words { get; set; }
        public List<string> WordsInEnglish { get; set; }

        public List<FillTheGapTaskViewModel>? FillTasks { get; set; }

        public ThemeViewModel() { }
        public ThemeViewModel(Theme theme)
        {
            Id = theme.Id;
            Name = theme.Name;
            Description = theme.Description;

            if(theme.Words != null)
            {
                Words = theme.Words.Select(w => new WordViewModel(w)).ToList();
                WordsInEnglish = theme.Words.Select(w => w.English).ToList();
            }

            if(theme.FillTasks != null)
            {
                FillTasks = theme.FillTasks.Select(f => new FillTheGapTaskViewModel(f)).ToList();
            }
        }
    }
}
