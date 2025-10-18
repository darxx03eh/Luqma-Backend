using FluentValidation;
using Luqma.Core.Features.KitchenItems.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.KitchenItems.Commands.Validators
{
    public class UpdateKitchenItemsStatusValidator : AbstractValidator<UpdateKitchenItemsStatusCommand>
    {
        public UpdateKitchenItemsStatusValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage(SharedResponseKeys.StatusIsRequired)
                .Must(s => s == "Available" || s == "Unavailable")
                .WithMessage(SharedResponseKeys.StatusMustBeEitherAvailableOrUnavailable);
        }
    }
}
