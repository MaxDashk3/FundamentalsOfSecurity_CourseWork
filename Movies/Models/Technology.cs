using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
	public class Technology
	{
        public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		public IEnumerable<Hall>? Halls { get; set; }
    }
}
