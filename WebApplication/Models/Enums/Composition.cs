using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Enums
{
	public enum Composition
	{
		[Display(Name = "Одно место")]
		OnePlace = 1,

		[Display(Name = "Несколько мест")]
		SeveralPlaces = 2,

		[Display(Name = "Конверт")]
		Envelope = 3
	}
}