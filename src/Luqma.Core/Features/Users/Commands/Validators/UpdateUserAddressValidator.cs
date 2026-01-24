using FluentValidation;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.Users.Commands.Validators
{
    public class UpdateUserAddressValidator : AbstractValidator<UpdateUserAddressCommand>
    {
        public UpdateUserAddressValidator() => ApplyValidationRules();
        private void ApplyValidationRules()
        {
            RuleFor(user => user.City)
                .NotEmpty().WithMessage(SharedResponseKeys.CityNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.CityNotNull);
            RuleFor(user => user.State)
                .NotEmpty().WithMessage(SharedResponseKeys.StateNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.StateNotNull);
            RuleFor(user => user.Street)
                .NotEmpty().WithMessage(SharedResponseKeys.StreetNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.StreetNotNull);
        }
    }
}
