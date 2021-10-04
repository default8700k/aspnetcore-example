using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Net;
using WebApplication.Core.Interfaces;

using Display = System.ComponentModel.DataAnnotations.DisplayAttribute;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WebApplication.Models
{
	public class Order : IModelValidator
	{
		[Display(Name = "Номер")]
		public Int32 Id { get; set; }

		[Display(Name = "Номер груза")]
		public Int32 CargoId { get; set; }

		[Display(Name = "Номер отправителя")]
		public Int32 SenderId { get; set; }

		[Display(Name = "Номер получателя")]
		public Int32 ReceiverId { get; set; }

		[Display(Name = "Номер плательщика")]
		public Int32 PayerId { get; set; }

		[Display(Name = "Ip-адрес")]
		public IPAddress Ip { get; set; }

		[Display(Name = "Дополнительная информация", Prompt = "Введите дополнительную информацию")]
		public String Comment { get; set; }

		[Display(Name = "Время создания")]
		public DateTime DateTime { get; set; }

		public Cargo Cargo { get; set; }
		public Sender Sender { get; set; }
		public Receiver Receiver { get; set; }
		public Payer Payer { get; set; }

		public ValidationResult Validate()
		{
			foreach (var action in new Func<ValidationResult>[]
			{
				() => { return Cargo.Validate(); },
				() => { return Sender.Validate(); },
				() => { return Receiver.Validate(); },
				() => { return Payer.Validate(); }
			})
			{
				var validation = action.Invoke();
				if (validation.IsValid == false)
				{
					return validation;
				}
			}

			var validator = new Validator();
			return validator.Validate(this);
		}

		public static void SetModelBuilder(EntityTypeBuilder<Order> entity)
		{
			entity.Property(p => p.Comment).HasMaxLength(512);

			entity.HasOne(p => p.Cargo).WithOne(p => p.Order).HasForeignKey<Order>(p => p.CargoId);
			entity.HasOne(p => p.Sender).WithOne(p => p.Order).HasForeignKey<Order>(p => p.SenderId);
			entity.HasOne(p => p.Receiver).WithOne(p => p.Order).HasForeignKey<Order>(p => p.ReceiverId);
			entity.HasOne(p => p.Payer).WithOne(p => p.Order).HasForeignKey<Order>(p => p.PayerId);
		}

		private class Validator : AbstractValidator<Order>
		{
			public Validator()
			{
				RuleFor(p => p.Comment).MaximumLength(512);
			}
		}
	}
}