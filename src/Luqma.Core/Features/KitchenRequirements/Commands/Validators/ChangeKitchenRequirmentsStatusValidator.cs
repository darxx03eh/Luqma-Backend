using FluentValidation;
using Luqma.Core.Features.KitchenRequirements.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.KitchenRequirements.Commands.Validators
{
    public class ChangeKitchenRequirmentsStatusValidator : AbstractValidator<ChangeKitchenRequirementsStatusCommand>
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
