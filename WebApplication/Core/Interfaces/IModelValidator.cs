using FluentValidation.Results;

namespace WebApplication.Core.Interfaces
{
	public interface IModelValidator
	{
		public ValidationResult Validate();
	}
}