using FluentValidation;
using PHP.AzureApiExample.Api.ViewModels;

namespace PHP.AzureApiExample.Api.Validators
{
    /// <summary>
    /// A simple fluent validator for the person view model.
    /// </summary>
    public class PersonJsonViewModelValidator : AbstractValidator<PersonJsonViewModel>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PersonJsonViewModelValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
        }
    }
}
