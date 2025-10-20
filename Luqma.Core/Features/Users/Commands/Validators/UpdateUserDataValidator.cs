using FluentValidation;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Luqma.Core.Features.Users.Commands.Validators
{
    public class UpdateUserDataValidator : AbstractValidator<UpdateUserDataCommand>
    {
        private readonly UserManager<LuqmaUser> userManager;
        public UpdateUserDataValidator(UserManager<LuqmaUser> userManager)
        {
            this.userManager = userManager;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(user => user.UserData.FirstName)
                .NotEmpty().WithMessage(SharedResponseKeys.FirstNameNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.FirstNameNotNull);

            RuleFor(user => user.UserData.LastName)
                .NotEmpty().WithMessage(SharedResponseKeys.LastNameNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.LastNameNotNull);

            RuleFor(user => user.UserData.Email)
                .NotEmpty().WithMessage(SharedResponseKeys.EmailNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.EmailNotNull)
                .EmailAddress().WithMessage(SharedResponseKeys.NotValidEmail);

            RuleFor(user => user.UserData.UserName)
                .NotEmpty().WithMessage(SharedResponseKeys.UserNameNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.UserNameNotNull);

            RuleFor(user => user.UserData.Gender)
                .IsInEnum().WithMessage(SharedResponseKeys.InvalidGender);

            RuleFor(user => user.UserData.BirthDate)
                .LessThan(DateTime.Now).WithMessage(SharedResponseKeys.BirthDateMustBeInThePast)
                .GreaterThan(DateTime.Now.AddYears(-120)).WithMessage(SharedResponseKeys.BirthDateIsUnrealistic);

        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(user => user.UserData.UserName)
                .MustAsync(async (model, key, cancellation) =>
                {
                    var username = await userManager.FindByNameAsync(key);
                    return username is null || username.Id.Equals(model.UserData.UserId);
                }).WithMessage(SharedResponseKeys.UserNameAlreadyExist);
            RuleFor(user => user.UserData.Email)
                .MustAsync(async (model, key, cancellation) =>
                {
                    var email = await userManager.FindByEmailAsync(key);
                    return email is null || email.Id.Equals(model.UserData.UserId);
                }).WithMessage(SharedResponseKeys.EmailAlreadyExist);
        }
    }
}
