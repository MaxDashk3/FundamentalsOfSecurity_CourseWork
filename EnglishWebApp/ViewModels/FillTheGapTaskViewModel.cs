using EnglishWebApp.Models;

namespace EnglishWebApp.ViewModels
{
    public class FillTheGapTaskViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int? ThemeId { get; set; }
        public string? Theme { get; set; }
        public List<Word>? Words { get; set; }
        public List<string>? CorrectWords { get; set; }
        public List<string>? TextLines { get; set; }

        public FillTheGapTaskViewModel() { }
        public FillTheGapTaskViewModel(FillTheGapTask task)
        {
            Id = task.Id;
            Name = task.Name;
            Text = task.Text;
            ThemeId = task.ThemeId;

            var AllLines = Text.Split("~");

            for (int i = 0; i < AllLines.Length; i++)
            {
                if (i % 2 == 0) TextLines.Add(AllLines[i]);
                else CorrectWords.Add(AllLines[i]);
            }


            if (task.Theme != null)
            {
                Theme = task.Theme.Name;
            }
            if (task.Words != null)
            {
                Words = task.Words.ToList();
            }
        }
    }
}
