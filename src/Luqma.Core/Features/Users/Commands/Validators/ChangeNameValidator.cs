using FluentValidation;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;

namespace Luqma.Core.Features.Users.Commands.Validators
{
    public class ChangeNameValidator : AbstractValidator<ChangeNameCommand>
    {
        private readonly IUserService userService;

        public ChangeNameValidator(IUserService userService)
        {
            this.userService = userService;
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {

            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage(SharedResponseKeys.FirstNameNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.FirstNameNotNull);

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage(SharedResponseKeys.LastNameNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.LastNameNotNull);

        }
    }
}
