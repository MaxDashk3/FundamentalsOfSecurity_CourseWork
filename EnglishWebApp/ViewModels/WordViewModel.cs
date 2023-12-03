using EnglishWebApp.Models;

namespace EnglishWebApp.ViewModels
{
    public class WordViewModel
    {
        public int Id { get; set; }
        public string English { get; set; }
        public string Ukrainian { get; set; }
        public string Transcript { get; set; }
        public string Meaning { get; set; }
        public string PathToImage { get; set; }
        public int? ThemeId { get; set; }
        public string? Theme { get; set; }

        public WordViewModel() { }

        public WordViewModel(Word word)
        {
            Id = word.Id;
            English = word.English;
            Ukrainian = word.Ukrainian;
            Transcript = word.Transcript;
            Meaning = word.Meaning;
            PathToImage = word.PathToImage;
            ThemeId = word.ThemeId;

            if (word.Theme != null)
            {
                Theme = word.Theme.Name;
            }

        }
    }
}
