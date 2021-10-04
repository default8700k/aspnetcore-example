using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebApplication.Core.Interfaces;
using WebApplication.Models.Enums;

using Display = System.ComponentModel.DataAnnotations.DisplayAttribute;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WebApplication.Models
{
	public class Receiver : IModelValidator
	{
		[Display(Name = "Номер")]
		public Int32 Id { get; set; }

		[Display(Name = "Город получения")]
		public City? City { get; set; }

		[Display(Name = "Получатель груза", Prompt = "Введите получателя груза")]
		public String Name { get; set; }

		[Display(Name = "Адрес получателя", Prompt = "Введите адрес получателя")]
		public String Address { get; set; }

		[Display(Name = "Время работы от", Prompt = "чч:мм")]
		public String TimeStart { get; set; }

		[Display(Name = "Время работы до", Prompt = "чч:мм")]
		public String TimeEnd { get; set; }

		[Display(Name = "ИНН получателя", Prompt = "Введите ИНН получателя")]
		public String Inn { get; set; }

		[Display(Name = "Контактное лицо", Prompt = "Введите контактное лицо")]
		public String ContactName { get; set; }

		[Display(Name = "Контактный телефон", Prompt = "Введите контактный телефон")]
		public String ContactPhone { get; set; }

		[Display(Name = "Погрузочно-разгрузочные работы")]
		public Boolean IsUnloadingWorks { get; set; }

		[Display(Name = "Доставить груз до адреса получателя по времени")]
		public Boolean IsDeliveryOnTime { get; set; }

		public Order Order { get; set; }

		public ValidationResult Validate()
		{
			var validator = new Validator();
			return validator.Validate(this);
		}

		public static void SetModelBuilder(EntityTypeBuilder<Receiver> entity)
		{
			entity.Property(p => p.City).IsRequired();
			entity.Property(p => p.Name).HasMaxLength(256).IsRequired();

			entity.Property(p => p.Address).HasMaxLength(128);
			entity.Property(p => p.TimeStart).HasMaxLength(16);
			entity.Property(p => p.TimeEnd).HasMaxLength(16);

			entity.Property(p => p.Inn).HasMaxLength(64);
			entity.Property(p => p.ContactName).HasMaxLength(128);
			entity.Property(p => p.ContactPhone).HasMaxLength(32);
		}

		private class Validator : AbstractValidator<Receiver>
		{
			public Validator()
			{
				RuleFor(p => p.City).NotNull();
				RuleFor(p => p.Name).NotEmpty().MaximumLength(256);

				RuleFor(p => p.Address).MaximumLength(128);
				RuleFor(p => p.TimeStart).MaximumLength(16);
				RuleFor(p => p.TimeEnd).MaximumLength(16);

				RuleFor(p => p.Inn).MaximumLength(64);
				RuleFor(p => p.ContactName).MaximumLength(128);
				RuleFor(p => p.ContactPhone).MaximumLength(32);
			}
		}
	}
}