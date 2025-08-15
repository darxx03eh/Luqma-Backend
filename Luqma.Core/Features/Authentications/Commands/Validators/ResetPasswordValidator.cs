using FluentValidation;
using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Luqma.Core.Features.Authentications.Commands.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        private readonly UserManager<LuqmaUser> userManager;

        public ResetPasswordValidator(UserManager<LuqmaUser> userManager)
        {
            this.userManager = userManager;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(user => user.Token)
                .NotEmpty().WithMessage(SharedResponseKeys.PasswordResetTokenNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.PasswordResetTokenNotNull);
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(SharedResponseKeys.EmailNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.EmailNotNull)
                .EmailAddress().WithMessage(SharedResponseKeys.EmailNotNull);
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
        private void ApplyCustomValidationRules()
        {
            RuleFor(user => user.Email)
                .MustAsync(async (key, cancellation) =>
                {
                    var email = await userManager.FindByEmailAsync(key);
                    return email is not null;
                }).WithMessage(SharedResponseKeys.UserNotFound);
        }
    }
}
