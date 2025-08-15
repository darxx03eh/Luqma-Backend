using FluentValidation;
using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Luqma.Core.Features.Authentications.Commands.Validators
{
    public class SendForgetPasswordValidator : AbstractValidator<SendForgetPasswordCommand>
    {
        private readonly UserManager<LuqmaUser> userManager;

        public SendForgetPasswordValidator(UserManager<LuqmaUser> userManager)
        {
            this.userManager = userManager;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(SharedResponseKeys.EmailNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.EmailNotNull)
                .EmailAddress().WithMessage(SharedResponseKeys.NotValidEmail);
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
