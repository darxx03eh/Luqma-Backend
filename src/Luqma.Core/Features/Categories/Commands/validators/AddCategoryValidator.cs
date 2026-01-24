using FluentValidation;
using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Core.Features.Categories.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Categories.Commands.validators
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public AddCategoryValidator(ICategoryRepository categoryRepository)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _categoryRepository = categoryRepository;
        }
        public void ApplyValidationRules()
        {
            RuleFor(Category => Category.Title)
            .NotEmpty().WithMessage(SharedResponseKeys.TitleNotEmpty)
            .NotNull().WithMessage(SharedResponseKeys.TitleNotNull)
            .MaximumLength(100).WithMessage(SharedResponseKeys.TitleMaximumLength);

        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(category => category.Title)
                .MustAsync(async (key, cancellation) =>
                {
                    return !await _categoryRepository.IsTitleExistAsync(key);

                }).WithMessage(SharedResponseKeys.TitleIsAlreadyExist);


        }
    }
}
