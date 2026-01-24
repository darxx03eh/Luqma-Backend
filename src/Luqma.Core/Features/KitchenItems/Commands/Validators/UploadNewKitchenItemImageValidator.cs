using FluentValidation;
using Luqma.Core.Features.KitchenItems.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.KitchenItems.Commands.Validators
{
    public class UploadNewKitchenItemImageValidator : AbstractValidator<UploadNewKitchenItemImageCommand>
    {
        public UploadNewKitchenItemImageValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(image => image.Image.ContentType)
                .Must(ct => ct == "image/jpeg" || ct == "image/png" || ct == "image/webp")
                .WithMessage(SharedResponseKeys.OnlyJPEGPNGAndWebPFormatsAreAllowed);

            RuleFor(image => image.Image.Length)
                .LessThanOrEqualTo(2 * 1024 * 1024).WithMessage(SharedResponseKeys.ImageSizeMustNotExceed5MB);
        }
    }
}
