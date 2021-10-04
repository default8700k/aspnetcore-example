using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Net;
using WebApplication.Core.Interfaces;

using DisplayAttribute = System.ComponentModel.DataAnnotations.DisplayAttribute;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WebApplication.Models
{
	public class Call : IModelValidator
	{
		[Display(Name = "Номер")]
		public Int32 Id { get; set; }

		[Display(Name = "Ip-адрес")]
		public IPAddress Ip { get; set; }

		[Display(Name = "Имя", Prompt = "Введите имя")]
		public String Name { get; set; }

		[Display(Name = "Номер телефона", Prompt = "Введите номер телефона")]
		public String Phone { get; set; }

		[Display(Name = "Время создания")]
		public DateTime DateTime { get; set; }

		public ValidationResult Validate()
		{
			var validator = new Validator();
			return validator.Validate(this);
		}

		public static void SetModelBuilder(EntityTypeBuilder<Call> entity)
		{
			entity.Property(p => p.Name).HasMaxLength(32).IsRequired();
			entity.Property(p => p.Phone).HasMaxLength(32).IsRequired();
		}

		private class Validator : AbstractValidator<Call>
		{
			public Validator()
			{
				RuleFor(p => p.Name).NotEmpty().MaximumLength(32);
				RuleFor(p => p.Phone).NotEmpty().MaximumLength(32);
			}
		}
	}
}