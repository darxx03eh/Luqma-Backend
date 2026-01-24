using FluentValidation;
using Luqma.Core.Features.Feebacks.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.Feebacks.Commands.Validators
{
    public class AddNewFeedbackValidator : AbstractValidator<AddNewFeedbackCommand>
    {
        public AddNewFeedbackValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(x => x.ItemId)
                .NotNull().WithMessage(SharedResponseKeys.ItemIdIsRequired)
                .NotEmpty().WithMessage(SharedResponseKeys.ItemIdNotEmpty)
                .GreaterThan(0).WithMessage(SharedResponseKeys.ItemIdMustBeGreaterThanZero);

            RuleFor(x => x.Stars)
                .NotNull().WithMessage(SharedResponseKeys.StarsIsRequired)
                .NotEmpty().WithMessage(SharedResponseKeys.StarsNotEmpty)
                .InclusiveBetween(1, 5).WithMessage(SharedResponseKeys.StarsMustBeBetween1And5);

            RuleFor(x => x.Content)
                .MaximumLength(500).WithMessage(SharedResponseKeys.ContentMustNotExceed500Characters)
                .When(x => !string.IsNullOrWhiteSpace(x.Content));

        }
    }
}
