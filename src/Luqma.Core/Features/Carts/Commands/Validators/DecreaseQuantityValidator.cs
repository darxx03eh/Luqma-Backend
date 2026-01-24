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
   public class DecreaseQuantityValidator : AbstractValidator<DecreaseQuantityCommand>
    {
        private readonly ICartRepository _cartRepository;

        public DecreaseQuantityValidator(ICartRepository cartRepository)
        {
            ApplyCustomValidationRules();
            _cartRepository = cartRepository;
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(iqv => iqv.ItemId)
           .MustAsync(async (key, cancellation) =>
           {
               return await _cartRepository.CheckQuantity(key);

           }).WithMessage(SharedResponseKeys.NoDecreaseQuantity);
        }
    }


    }

