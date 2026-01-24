using FluentValidation;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Luqma.Core.Features.Users.Commands.Validators
{
    public class ChangeUserNameValidator : AbstractValidator<ChangeUserNameCommand>
    {
        private readonly IUserService userService;

        public ChangeUserNameValidator(IUserService userService)
        {
            this.userService = userService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage(SharedResponseKeys.UserNameNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.UserNameNotNull);
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(user => user.UserName)
                .MustAsync(async (key, cancellation) =>
                {
                    return await userService.CheckUserNameAsync(key);
                }).WithMessage(SharedResponseKeys.UserNameAlreadyExist);
        }
    }
}
