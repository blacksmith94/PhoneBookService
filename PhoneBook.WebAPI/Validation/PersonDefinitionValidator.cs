using FluentValidation;
using PhoneBook.WebAPI.Definitions;

namespace PhoneBook.WebAPI.Validation
{
	public class PersonDefinitionValidator : AbstractValidator<PersonDefinition>
	{
		public PersonDefinitionValidator()
		{
			RuleFor(person => person.FirstName)
				.NotNull().WithMessage("First name cannot be null.");

			RuleFor(person => person.SecondName)
				.NotNull().WithMessage("Second name cannot be null.");

			RuleFor(person => person.PhoneNumber)
				.NotNull().WithMessage("Phone number cannot be null.");

			RuleFor(person => person.Address)
				.NotNull().WithMessage("Address cannot be null.");
		}
	}
}
