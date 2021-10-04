using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Enums
{
	public enum Dimensions
	{
		[Display(Name = "До одного метра")]
		UpToOne = 1,

		[Display(Name = "До двух метров")]
		UpToTwo = 2,

		[Display(Name = "До трех метров")]
		UpToThree = 3,

		[Display(Name = "До четырех метров")]
		UpToFour = 4,

		[Display(Name = "До пяти метров")]
		UpToFive = 5,

		[Display(Name = "До шести метров")]
		UpToSix = 6,

		[Display(Name = "Более шести метров")]
		MoreThanSix = 7
	}
}