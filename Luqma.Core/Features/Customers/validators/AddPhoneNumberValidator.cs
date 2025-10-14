using FluentValidation;
using Luqma.Core.Features.Customers.commands.Models;
using Luqma.Core.ResponseKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Customers.validators
{
   public class AddPhoneNumberValidator: AbstractValidator<AddPhoneNumberCommand>
    {

        public AddPhoneNumberValidator()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
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
