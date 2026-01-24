using FluentValidation;
using Luqma.Core.Features.Payments.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Payments.Commands.Validators
{
   public class AddPaymentValidator : AbstractValidator<AddPaymentCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public AddPaymentValidator(IOrderRepository orderRepository)
        {
            ApplyCustomValidationRules();
            _orderRepository = orderRepository;
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(adc => adc.OrderId)
                .MustAsync(async (key, cancellation) =>
                {
                    return await _orderRepository.IsOrderIdExistInOrders(key);
                }).WithMessage(SharedResponseKeys.NotExistOrderId);
        }
    }
}
