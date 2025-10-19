using FluentValidation;
using Luqma.Core.Features.KitchenRequirments.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.KitchenRequirments.Commands.Validators
{
    public class ChangeKitchenRequirmentsStatusValidator : AbstractValidator<ChangeKitchenRequirmentsStatusCommand>
    {
        public ChangeKitchenRequirmentsStatusValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage(SharedResponseKeys.StatusIsRequired)
                .Must(s => s == "Accepted" || s == "Rejected")
                .WithMessage(SharedResponseKeys.StatusMustBeEitherAcceptedOrRejectede);
        }
    }
}
