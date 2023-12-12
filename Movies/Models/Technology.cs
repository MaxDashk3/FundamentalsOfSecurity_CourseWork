using Microsoft.CodeAnalysis.Completion;
using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
	public class Technology
	{
        public int Id { get; set; }
		[Required]
		[Display(Prompt = "Enter technology here", Name = "Technology name")]
		public string Name { get; set; }

		public IEnumerable<Hall>? Halls { get; set; }
    }
}
