using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebApplication.Core.Interfaces;

using Display = System.ComponentModel.DataAnnotations.DisplayAttribute;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WebApplication.Models
{
	public class Payer : IModelValidator
	{
		[Display(Name = "Номер")]
		public Int32 Id { get; set; }

		[Display(Name = "Плательщик", Prompt = "Введите плательщика")]
		public String Name { get; set; }

		[Display(Name = "Адрес электронной почты", Prompt = "Введите адрес электронной почты")]
		public String Email { get; set; }

		[Display(Name = "ИНН плательщика", Prompt = "Введите ИНН плательщика")]
		public String Inn { get; set; }

		[Display(Name = "Контактное лицо", Prompt = "Введите контактное лицо")]
		public String ContactName { get; set; }

		[Display(Name = "Контактный телефон", Prompt = "Введите контактный телефон")]
		public String ContactPhone { get; set; }

		public Order Order { get; set; }

		public ValidationResult Validate()
		{
			var validator = new Validator();
			return validator.Validate(this);
		}

		public static void SetModelBuilder(EntityTypeBuilder<Payer> entity)
		{
			entity.Property(p => p.Name).HasMaxLength(256).IsRequired();
			entity.Property(p => p.Email).HasMaxLength(128);

			entity.Property(p => p.Inn).HasMaxLength(64);
			entity.Property(p => p.ContactName).HasMaxLength(128);
			entity.Property(p => p.ContactPhone).HasMaxLength(32);
		}

		private class Validator : AbstractValidator<Payer>
		{
			public Validator()
			{
				RuleFor(p => p.Name).NotEmpty().MaximumLength(256);
				RuleFor(p => p.Email).MaximumLength(128);

				RuleFor(p => p.Inn).MaximumLength(64);
				RuleFor(p => p.ContactName).MaximumLength(128);
				RuleFor(p => p.ContactPhone).MaximumLength(32);
			}
		}
	}
}