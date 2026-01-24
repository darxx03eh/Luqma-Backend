using FluentValidation;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.Users.Commands.Validators
{
    public class ChangePasswordForUserByManagerValidator : AbstractValidator<ChangePasswordForUserByManagerCommand>
    {
        public ChangePasswordForUserByManagerValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(user => user.UserId)
                .NotEmpty().WithMessage(SharedResponseKeys.UserIdIsRequired)
                .NotNull().WithMessage(SharedResponseKeys.UserIdNotNull);

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(SharedResponseKeys.PasswordNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.PasswordNotNull)
                .MinimumLength(6).WithMessage(SharedResponseKeys.PasswordMinimumLength);

            RuleFor(user => user.ConfirmPassword)
                .NotEmpty().WithMessage(SharedResponseKeys.ConfirmPasswordNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.ConfirmPasswordNotNull)
                .MinimumLength(6).WithMessage(SharedResponseKeys.ConfirmPasswordMinimumLength)
                .Equal(user => user.Password).WithMessage(SharedResponseKeys.ConfirmPasswordMustEqualToPassword);
        }
    }
}
