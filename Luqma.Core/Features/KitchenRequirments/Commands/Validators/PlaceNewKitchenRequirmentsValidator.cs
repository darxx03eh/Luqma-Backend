using FluentValidation;
using Luqma.Core.Features.KitchenRequirments.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.KitchenRequirments.Commands.Validators
{
    public class PlaceNewKitchenRequirmentsValidator : AbstractValidator<PlaceNewKitchenRequirmentsCommand>
    {
        public PlaceNewKitchenRequirmentsValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(x => x.RequirmentItems)
                .NotEmpty().WithMessage(SharedResponseKeys.RequirmentItemsAreRequired);

            RuleForEach(x => x.RequirmentItems).ChildRules(items =>
            {
                items.RuleFor(i => i.ItemId)
                    .GreaterThan(0).WithMessage(SharedResponseKeys.ItemIdMustBeGreaterThanZero);

                items.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage(SharedResponseKeys.QuantityMustBeGreaterThanZero);

                items.RuleFor(i => i.Note)
                    .MaximumLength(500).WithMessage(SharedResponseKeys.NoteMustNotExceed500Characters)
                    .When(x => !string.IsNullOrWhiteSpace(x.Note));
            });

            RuleFor(x => x.Note)
                .MaximumLength(500)
                .WithMessage(SharedResponseKeys.NoteMustNotExceed500Characters)
                .When(x => !string.IsNullOrWhiteSpace(x.Note));
        }
    }
}
