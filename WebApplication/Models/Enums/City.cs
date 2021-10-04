using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Enums
{
	public enum City
	{
		[Display(Name = "Москва")]
		Moscow = 1,

		[Display(Name = "Липецк")]
		Lipetsk = 2
	}
}