using FluentValidation;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;

namespace Luqma.Core.Features.Users.Commands.Validators
{
    public class DeactivateUserValidator : AbstractValidator<DeactivateUserCommand>
    {
        private readonly IUserService userService;

        public DeactivateUserValidator(IUserService userService)
        {
            this.userService = userService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(date => date.UserId)
                .NotEmpty().WithMessage(SharedResponseKeys.UserIdIsRequired)
                .NotNull().WithMessage(SharedResponseKeys.UserIdNotNull);
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(date => date.UserId)
                .MustAsync(async (key, cancellation) =>
                {
                    return await userService.IsUserExistAsync(key);
                }).WithMessage(SharedResponseKeys.UserNotExist);
        }
    }
}
