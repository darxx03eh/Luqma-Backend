using FluentValidation;
using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Luqma.Core.Features.Authentications.Commands.Validators
{
    public class SendConfirmationEmailValidator : AbstractValidator<SendConfirmationEmailCommand>
    {
        private readonly UserManager<LuqmaUser> userManager;

        public SendConfirmationEmailValidator(UserManager<LuqmaUser> userManager)
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
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(user => user.UserName)
                .MustAsync(async (key, cancellation) =>
                {
                    var username = await userManager.FindByNameAsync(key);
                    return username is not null;
                }).WithMessage(SharedResponseKeys.UserNotFound);
        }
    }
}
