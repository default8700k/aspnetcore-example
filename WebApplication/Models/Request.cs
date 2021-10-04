using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Net;
using WebApplication.Core.Interfaces;

using Display = System.ComponentModel.DataAnnotations.DisplayAttribute;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WebApplication.Models
{
	public class Request : IModelValidator
	{
		[Display(Name = "Номер")]
		public Int32 Id { get; set; }

		[Display(Name = "Ip-адрес")]
		public IPAddress Ip { get; set; }

		[Display(Name = "Url")]
		public String Url { get; set; }

		[Display(Name = "Method")]
		public String Method { get; set; }

		[Display(Name = "User-Agent")]
		public String UserAgent { get; set; }

		[Display(Name = "Время запроса")]
		public DateTime DateTime { get; set; }

		public ValidationResult Validate()
		{
			throw new NotImplementedException();
		}

		public static void SetModelBuilder(EntityTypeBuilder<Request> entity)
		{
			entity.Property(p => p.Url).HasMaxLength(128);
			entity.Property(p => p.Method).HasMaxLength(16);
			entity.Property(p => p.UserAgent).HasMaxLength(256);
		}
	}
}