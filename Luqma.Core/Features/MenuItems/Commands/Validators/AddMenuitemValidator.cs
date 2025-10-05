using FluentValidation;
using Luqma.Core.Features.Categories.Commands.Models;
using Luqma.Core.Features.MenuItems.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.MenuItems.Commands.Validators
{
    public class AddMenuitemValidator : AbstractValidator<AddMenuItemCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMenuRepository _menuRepository;

        public AddMenuitemValidator(ICategoryRepository categoryRepository,IMenuRepository menuRepository)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _categoryRepository = categoryRepository;
            _menuRepository = menuRepository;
        }
        public void ApplyValidationRules()
        {
            RuleFor(mi => mi.Item)
            .NotEmpty().WithMessage(SharedResponseKeys.ItemNotEmpty)
            .NotNull().WithMessage(SharedResponseKeys.ItemNotNull)
            .MaximumLength(100).WithMessage(SharedResponseKeys.ItemMaximimLength);
            RuleFor(mi => mi.Description)
                .MaximumLength(500).WithMessage(SharedResponseKeys.DescriptionMaximumLength);
            RuleFor(mi => mi.Price)
                .NotEmpty().WithMessage(SharedResponseKeys.PriceNotEmpty)
                .NotNull().WithMessage(SharedResponseKeys.PriceNotNull)
                .InclusiveBetween(0.01d, 99999999.99d).WithMessage(SharedResponseKeys.PriceHasPrecision)
                .GreaterThanOrEqualTo(0).WithMessage(SharedResponseKeys.PriceGreaterorEqualZero);
            RuleFor(mi => mi.Discount)
                .GreaterThanOrEqualTo(0).WithMessage(SharedResponseKeys.DiscountGreaterzeroandless100)
                .LessThanOrEqualTo(100).WithMessage(SharedResponseKeys.DiscountGreaterzeroandless100);
            RuleFor(mi => mi.IsVegetarian)
                .NotNull().WithMessage(SharedResponseKeys.IsVegetarianNotNull);

        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(mi => mi.CategoryId)
                 .MustAsync(async (key, cancellation) =>
                 {
                     return await _categoryRepository.IsIdExistInCategoryAsync(key);

                 }).WithMessage(SharedResponseKeys.CategoryIdNotFound);

            RuleFor(mi => mi.MenuId)
                 .MustAsync(async (key, cancellation) =>
                 {
                     return await _menuRepository.IsIdInMenuAsync(key);

                 }).WithMessage(SharedResponseKeys.NotFoundMenuId);
        }

    }
}
