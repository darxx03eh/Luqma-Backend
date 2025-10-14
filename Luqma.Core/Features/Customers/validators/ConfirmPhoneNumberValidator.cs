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
   public  class ConfirmPhoneNumberValidator : AbstractValidator<ConfirmPhoneNumberCodeCommand>
    {
        public ConfirmPhoneNumberValidator()
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
            RuleFor(customer => customer.Code)
               .NotEmpty().WithMessage(SharedResponseKeys.CodeNotEmpty)
               .NotNull().WithMessage(SharedResponseKeys.CodeNotNull)
               .MinimumLength(6).WithMessage(SharedResponseKeys.CodeLessThan6)
               .MaximumLength(6).WithMessage(SharedResponseKeys.CodeGreaterThan6);
             
               
        }
    }
}
