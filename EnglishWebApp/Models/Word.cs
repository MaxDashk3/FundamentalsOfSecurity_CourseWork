using EnglishWebApp.ViewModels;

namespace EnglishWebApp.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string English { get; set; }
        public string Ukrainian { get; set; }
        public string Transcript { get; set; }
        public string Meaning { get; set; }
        public string PathToImage { get; set; }
        
        public int? ThemeId { get; set; }

        public Theme? Theme { get; set; }
        public IEnumerable<FillTheGapTask> FillTasks { get; set; }

        public Word() { }
        public Word(WordViewModel model)
        {
            Id = model.Id;
            English = model.English;
            Ukrainian = model.Ukrainian;
            Transcript = model.Transcript;
            Meaning = model.Meaning;
            PathToImage = model.PathToImage;
            ThemeId = model.ThemeId;
        }
    }
}
