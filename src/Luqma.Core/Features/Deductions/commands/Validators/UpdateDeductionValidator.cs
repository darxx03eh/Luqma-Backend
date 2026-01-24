using FluentValidation;
using Luqma.Core.Features.Deductions.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;

namespace Luqma.Core.Features.Deductions.Commands.Validators
{
    public class UpdateDeductionValidator : AbstractValidator<UpdateDeductionCommand>
    {
        private readonly IUserService userService;

        public UpdateDeductionValidator(IUserService userService)
        {
            this.userService = userService;
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(user => user.DeductionRate)
                .NotEmpty().WithMessage(SharedResponseKeys.DeductionRateNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.DeductionRateNotNull)
                .GreaterThanOrEqualTo(0).WithMessage(SharedResponseKeys.DeductionRateShouldBeGreaterThanOrEqualToZero);
        }
    }
}
