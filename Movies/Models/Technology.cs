using Microsoft.CodeAnalysis.Completion;
using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
	public class Technology
	{
        public int Id { get; set; }
		[Required]
		[Display(Prompt = "Enter name here")]
		public string Name { get; set; }

		public IEnumerable<Hall>? Halls { get; set; }
    }
}
