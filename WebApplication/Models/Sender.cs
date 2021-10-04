using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebApplication.Core.Interfaces;
using WebApplication.Models.Enums;

using Display = System.ComponentModel.DataAnnotations.DisplayAttribute;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WebApplication.Models
{
	public class Sender : IModelValidator
	{
		[Display(Name = "Номер")]
		public Int32 Id { get; set; }

		[Display(Name = "Город отправления")]
		public City? City { get; set; }

		[Display(Name = "Отправитель груза", Prompt = "Введите отправителя груза")]
		public String Name { get; set; }

		[Display(Name = "Адрес отправителя", Prompt = "Введите адрес отправителя")]
		public String Address { get; set; }

		[Display(Name = "Время работы от", Prompt = "чч:мм")]
		public String TimeStart { get; set; }

		[Display(Name = "Время работы до", Prompt = "чч:мм")]
		public String TimeEnd { get; set; }

		[Display(Name = "ИНН отправителя", Prompt = "Введите ИНН отправителя")]
		public String Inn { get; set; }

		[Display(Name = "Контактное лицо", Prompt = "Введите контактное лицо")]
		public String ContactName { get; set; }

		[Display(Name = "Контактный телефон", Prompt = "Введите контактный телефон")]
		public String ContactPhone { get; set; }

		[Display(Name = "Заказать пропуск на въезд транспорта")]
		public Boolean IsTicket { get; set; }

		[Display(Name = "Погрузочно-разгрузочные работы")]
		public Boolean IsLoadingWorks { get; set; }

		[Display(Name = "Упаковка в жесткую обрешетку")]
		public Boolean IsPacking { get; set; }

		public Order Order { get; set; }

		public ValidationResult Validate()
		{
			var validator = new Validator();
			return validator.Validate(this);
		}

		public static void SetModelBuilder(EntityTypeBuilder<Sender> entity)
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

		private class Validator : AbstractValidator<Sender>
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