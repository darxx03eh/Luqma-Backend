using FluentValidation;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;
using Microsoft.Extensions.Localization;

namespace Luqma.Core.Features.Users.Commands.Validators
{
    public class UploadProfileImageValidator : AbstractValidator<UploadProfileImageCommand>
    {
        public UploadProfileImageValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(image => image.ProfileImage)
                .NotNull().WithMessage(SharedResponseKeys.ImageIsRequired);
            RuleFor(image => image.ProfileImage.Length)
                .LessThanOrEqualTo(5 * 1024 * 1024).WithMessage(SharedResponseKeys.ImageSizeMustNotExceed5MB);
            RuleFor(image => image.ProfileImage.ContentType)
                .Must(ct => ct == "image/jpeg" || ct == "image/png" || ct == "image/webp")
                .WithMessage(SharedResponseKeys.OnlyJPEGPNGAndWebPFormatsAreAllowed);
        }
    }
}
