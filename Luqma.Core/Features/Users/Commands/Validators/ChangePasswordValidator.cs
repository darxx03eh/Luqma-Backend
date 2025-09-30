using FluentValidation;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Luqma.Core.Features.Users.Commands.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        private readonly IUserService userService;

        public ChangePasswordValidator(IUserService userService)
        {
            this.userService = userService;
            ApplyCustomValidationRules();
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            
            RuleFor(user => user.NewPassword)
                .NotEmpty().WithMessage(SharedResponseKeys.PasswordNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.PasswordNotNull)
                .MinimumLength(6).WithMessage(SharedResponseKeys.PasswordMinimumLength);

            RuleFor(user => user.NewPasswordConfirm)
                .NotEmpty().WithMessage(SharedResponseKeys.ConfirmPasswordNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.ConfirmPasswordNotNull)
                .MinimumLength(6).WithMessage(SharedResponseKeys.ConfirmPasswordMinimumLength)
                .Equal(user => user.NewPassword).WithMessage(SharedResponseKeys.ConfirmPasswordMustEqualToPassword);

        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(user => user.CurrentPassword)
                .MustAsync(async (key, cancellation) =>
                {
                    return await userService.ChecPasswordAsync(key);
                }).WithMessage(SharedResponseKeys.CurrentPasswordWrong);

            RuleFor(user => user.NewPassword)
                .MustAsync(async (key, cancellation) =>
                {
                    return !await userService.ChecPasswordAsync(key);
                }).WithMessage(SharedResponseKeys.PasswordShouldNotEqualOldOne);
        }
    }
}
