using FluentValidation;
using Luqma.Core.Features.KitchenItems.Commands.Models;
using Luqma.Core.ResponseKeys;
using Microsoft.AspNetCore.Http.HttpResults;
using Twilio.TwiML.Voice;

namespace Luqma.Core.Features.KitchenItems.Commands.Validators
{
    public class AddKitchenItemsValidator : AbstractValidator<AddKitchenItemsCommand>
    {
        public AddKitchenItemsValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(x => x.Item)
                .NotEmpty().WithMessage(SharedResponseKeys.ItemNameIsRequired)
                .MaximumLength(100).WithMessage(SharedResponseKeys.ItemNameMustNotExceed100Characters);

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage(SharedResponseKeys.StatusIsRequired)
                .Must(s => s == "Available" || s == "Unavailable")
                .WithMessage(SharedResponseKeys.StatusMustBeEitherAvailableOrUnavailable);

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage(SharedResponseKeys.UnitIsRequired)
                .MaximumLength(50).WithMessage(SharedResponseKeys.UnitMustNotExceed50Characters);

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage(SharedResponseKeys.QuantityMustBe0OrGreater);

            RuleFor(image => image.Image.ContentType)
                .Must(ct => ct == "image/jpeg" || ct == "image/png" || ct == "image/webp")
                .WithMessage(SharedResponseKeys.OnlyJPEGPNGAndWebPFormatsAreAllowed)
                .When(image => image.Image is not null);

            RuleFor(image => image.Image.Length)
                .LessThanOrEqualTo(2 * 1024 * 1024).WithMessage(SharedResponseKeys.ImageSizeMustNotExceed5MB)
                .When(image => image.Image is not null);

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage(SharedResponseKeys.NoteMustNotExceed500Characters)
                .When(x => !string.IsNullOrWhiteSpace(x.Note));
        }
    }
}
