using FluentValidation;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.Users.Commands.Validators
{
    public class ChangeSalaryValidator : AbstractValidator<ChangeSalaryCommand>
    {
        public ChangeSalaryValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(user => user.UserId)
                .NotEmpty().WithMessage(SharedResponseKeys.UserIdIsRequired)
                .NotNull().WithMessage(SharedResponseKeys.UserIdNotNull);

            RuleFor(user => user.Salary)
                .GreaterThanOrEqualTo(0).WithMessage(SharedResponseKeys.SalaryMustBePositive);

        }
    }
}
