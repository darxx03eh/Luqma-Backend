using FluentValidation;
using Luqma.Core.Features.KitchenItems.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.KitchenItems.Commands.Validators
{
    public class UpdateKitchenItemsValidator : AbstractValidator<UpdateKitchenItemsCommand>
    {
        public UpdateKitchenItemsValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(x => x.Item)
                .NotEmpty().WithMessage(SharedResponseKeys.ItemNameIsRequired)
                .MaximumLength(100).WithMessage(SharedResponseKeys.ItemNameMustNotExceed100Characters);

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage(SharedResponseKeys.StatusIsRequired)
                .Must(s => s == "Available" || s == "Unavailable" || s == "Low")
                .WithMessage(SharedResponseKeys.StatusMustBeEitherAvailableOrUnavailableOrLow);

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage(SharedResponseKeys.UnitIsRequired)
                .MaximumLength(50).WithMessage(SharedResponseKeys.UnitMustNotExceed50Characters);

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage(SharedResponseKeys.QuantityMustBe0OrGreater);


            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage(SharedResponseKeys.NoteMustNotExceed500Characters)
                .When(x => !string.IsNullOrWhiteSpace(x.Note));
        }
    }
}
