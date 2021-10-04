using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebApplication.Core.Interfaces;
using WebApplication.Models.Enums;

using Display = System.ComponentModel.DataAnnotations.DisplayAttribute;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WebApplication.Models
{
	public class Cargo : IModelValidator
	{
		[Display(Name = "Номер")]
		public Int32 Id { get; set; }

		[Display(Name = "Дата забора груза")]
		public DateTime? Date { get; set; }

		[Display(Name = "Наименование груза", Prompt = "Введите наименование груза")]
		public String Name { get; set; }

		[Display(Name = "Состав груза")]
		public Composition? Composition { get; set; }

		[Display(Name = "Габариты груза")]
		public Dimensions? Dimensions { get; set; }

		[Display(Name = "Вес груза", Prompt = "Введите вес груза")]
		public Single? Weight { get; set; }

		[Display(Name = "Объем груза", Prompt = "Введите объем груза")]
		public Single? Volume { get; set; }

		[Display(Name = "Стоимость груза", Prompt = "Введите стоимость груза")]
		public Single? Price { get; set; }

		public Order Order { get; set; }

		public ValidationResult Validate()
		{
			var validator = new Validator();
			return validator.Validate(this);
		}

		public static void SetModelBuilder(EntityTypeBuilder<Cargo> entity)
		{
			entity.Property(p => p.Date).IsRequired();
			entity.Property(p => p.Name).HasMaxLength(256).IsRequired();

			entity.Property(p => p.Composition).IsRequired();
			entity.Property(p => p.Dimensions).IsRequired();
		}

		private class Validator : AbstractValidator<Cargo>
		{
			public Validator()
			{
				RuleFor(p => p.Date).NotNull();
				RuleFor(p => p.Name).NotEmpty().MaximumLength(256);

				RuleFor(p => p.Composition).NotNull();
				RuleFor(p => p.Dimensions).NotNull();
			}
		}
	}
}