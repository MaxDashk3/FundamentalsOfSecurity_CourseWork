using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.ViewModels
{
	public class HallViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "This field is required")]
		[Remote(action: "HallsValidation", controller: "Data", ErrorMessage = "The hall already exists!")]
		public string Name { get; set; }
		[Required(ErrorMessage = "This field is required")]
		[Range(5, 30, ErrorMessage = "There has to be from 5 to 30 rows")]
		public int Rows { get; set; }
		[Required(ErrorMessage = "This field is required")]
		[Range(5, 30, ErrorMessage = "There has to be from 5 to 30 seats per row")]
		public int SeatsPerRow { get; set; }

		public IEnumerable<Session>? Sessions { get; set; }
		public IEnumerable<Technology>? Technologies { get; set; }
		public List<string>? TechnologiesStrList { get; set; }
		public string? TechnologiesString { get; set; }

		public HallViewModel() { }
		public HallViewModel(Hall hall)
		{
			Id = hall.Id;
			Name = hall.Name;
			Rows = hall.Rows;
			SeatsPerRow = hall.SeatsPerRow;
			if (hall.Technologies != null)
			{
				TechnologiesStrList = hall.Technologies.Select(t => t.Name).ToList();
				TechnologiesString = "";
				foreach(var str in TechnologiesStrList)
				{
					TechnologiesString += (str+"; ");
				}
			}
		}
	}
}
