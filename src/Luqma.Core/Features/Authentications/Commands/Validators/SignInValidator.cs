using FluentValidation;
using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Luqma.Core.Features.Authentications.Commands.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        private readonly UserManager<LuqmaUser> userManager;

        public SignInValidator(UserManager<LuqmaUser> userManager)
        {
            this.userManager = userManager;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage(SharedResponseKeys.UserNameNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.UserNameNotNull);
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(SharedResponseKeys.PasswordNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.PasswordNotNull)
                .MinimumLength(6).WithMessage(SharedResponseKeys.PasswordMinimumLength);
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(user => user.UserName)
                .MustAsync(async (key, cancellation) =>
                {
                    var username = await userManager.FindByNameAsync(key);
                    return username is not null;
                }).WithMessage(SharedResponseKeys.UserNameNotExist);
        }
    }
}
