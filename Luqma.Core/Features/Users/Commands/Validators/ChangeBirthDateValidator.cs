using FluentValidation;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;

namespace Luqma.Core.Features.Users.Commands.Validators
{
    public class ChangeBirthDateValidator : AbstractValidator<ChangeBirthDateCommand>
    {
        public ChangeBirthDateValidator(IUserService userService)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(date => date.BirthDate)
                .NotEmpty().WithMessage(SharedResponseKeys.BirthDateIsRequired)
                .NotNull().WithMessage(SharedResponseKeys.BirthDateNotNull);
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(date => date.BirthDate)
                .MustAsync(async (key, cancellation) =>
                {
                    var age = DateTime.Today.Year - key.Year;
                    if (key > DateTime.Today.AddYears(-age))
                        age--;
                    return age >= 18;
                }).WithMessage(SharedResponseKeys.UserMustBe18OrOlder);
            RuleFor(date => date.BirthDate)
                .MustAsync(async (key, cancellation) =>
                {
                    return key.Date <= DateTime.Today;
                }).WithMessage(SharedResponseKeys.BirthDateCannotBeInFuture);
        }
    }
}
