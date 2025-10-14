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
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerDetailsCommand>
    {
        public UpdateCustomerValidator()
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
            RuleFor(customer => customer.FirstName)
                .NotNull().WithMessage(SharedResponseKeys.NotNullFirstName)
                .NotEmpty().WithMessage(SharedResponseKeys.NotNullLastName);
            RuleFor(customer => customer.LastName)
                .NotNull().WithMessage(SharedResponseKeys.NotNullLastName)
                .NotEmpty().WithMessage(SharedResponseKeys.NotEmptyLastName);
            RuleFor(customer => customer.gender)
                .NotNull().WithMessage(SharedResponseKeys.NotNullGender);
               
            RuleFor(customer => customer.City)
                .NotNull().WithMessage(SharedResponseKeys.NotNullCity)
                .NotEmpty().WithMessage(SharedResponseKeys.NotEmptyCity);
            RuleFor(customer => customer.State)
                .NotNull().WithMessage(SharedResponseKeys.NotNullState)
                .NotEmpty().WithMessage(SharedResponseKeys.NotEmptyState);
            RuleFor(customer => customer.Street)
                .NotNull().WithMessage(SharedResponseKeys.NotNullStreet)
                .NotEmpty().WithMessage(SharedResponseKeys.NotEmptyStreet);
             
        }

    }
}
