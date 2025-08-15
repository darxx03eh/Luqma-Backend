using FluentValidation;
using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.Authentications.Commands.Validators
{
    public class SendConfirmationCodeThenAdd : AbstractValidator<SendConfirmationCodeThenAddCommand>
    {
        public SendConfirmationCodeThenAdd()
            => ApplyValidationRules();
        void ApplyValidationRules()
        {
            RuleFor(phone => phone.PhoneNumber)
                .NotNull().WithMessage(SharedResponseKeys.PhoneNumberNotNull)
                .NotEmpty().WithMessage(SharedResponseKeys.PhoneNumberNotEmpty)
                .Must(phone => phone is not null && (phone.StartsWith("+970") || phone.StartsWith("+972")))
                .WithMessage(SharedResponseKeys.InvalidPhoneNumber)
                .MinimumLength(13).WithMessage(SharedResponseKeys.YourPhoneNumberMustHas13DigitsIncludesPlusNotLess)
                .MaximumLength(13).WithMessage(SharedResponseKeys.YourPhoneNumberMustHas13DigitsIncludesPlusNotMore);
        }
    }
}
