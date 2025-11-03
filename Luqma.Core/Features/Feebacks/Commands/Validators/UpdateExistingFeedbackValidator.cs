using FluentValidation;
using Luqma.Core.Features.Feebacks.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.Feebacks.Commands.Validators
{
    public class UpdateExistingFeedbackValidator : AbstractValidator<UpdateExistingFeedbackCommand>
    {
        public UpdateExistingFeedbackValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(x => x.FeedbackId)
                .NotNull().WithMessage(SharedResponseKeys.FeedbackIdIsRequired)
                .NotEmpty().WithMessage(SharedResponseKeys.FeedbackIdNotEmpty)
                .GreaterThan(0).WithMessage(SharedResponseKeys.FeedbackIdMustBeGreaterThanZero);

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
