using FluentValidation;
using Luqma.Core.Features.Carts.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Carts.Commands.Validators
{
    public class AddToCartValidator:AbstractValidator<AddToCartCommand>
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public AddToCartValidator(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
            ApplyCustomValidationRules();
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(atc => atc.ItemId)
            .MustAsync(async (key, cancellation) =>
            {
                return await _menuItemRepository.IsIdExistAsync(key);

            }).WithMessage(SharedResponseKeys.NotFounItemId);

        }
    }
}

