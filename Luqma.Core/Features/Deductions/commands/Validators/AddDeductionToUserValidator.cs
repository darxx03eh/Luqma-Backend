using FluentValidation;
using Luqma.Core.Features.Deductions.commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;

namespace Luqma.Core.Features.Deductions.commands.Validators
{
    public class AddDeductionToUserValidator : AbstractValidator<AddDeductionToUserCommand>
    {
        private readonly IUserService userService;

        public AddDeductionToUserValidator(IUserService userService)
        {
            this.userService = userService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(user => user.DeductionRate)
                .NotEmpty().WithMessage(SharedResponseKeys.DeductionRateNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.DeductionRateNotNull)
                .GreaterThanOrEqualTo(0).WithMessage(SharedResponseKeys.DeductionRateShouldBeGreaterThanOrEqualToZero);
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(user => user.UserId)
                .MustAsync(async (key, cancellation) =>
                {
                    return await userService.IsUserExistAsync(key);
                }).WithMessage(SharedResponseKeys.UserNotFound);
        }
    }
}
