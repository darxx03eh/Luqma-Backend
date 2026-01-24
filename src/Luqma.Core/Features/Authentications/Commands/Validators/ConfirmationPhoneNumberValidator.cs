using FluentValidation;
using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.Authentications.Commands.Validators
{
    public class ConfirmationPhoneNumberValidator : AbstractValidator<ConfirmationPhoneNumberCommand>
    {
        public ConfirmationPhoneNumberValidator() => ApplyValidationRules();
        public void ApplyValidationRules()
        {
            RuleFor(user => user.Code)
                .NotEmpty().WithMessage(SharedResponseKeys.CodeNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.CodeNotNull)
                .MinimumLength(6).WithMessage(SharedResponseKeys.CodeLessThan6)
                .MaximumLength(6).WithMessage(SharedResponseKeys.CodeGreaterThan6);
        }
    }
}
