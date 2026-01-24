using FluentValidation;
using Luqma.Core.Features.OrderItems.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.OrderItems.Queries.Validators
{
    public class ViewOrderDetailsValidator:AbstractValidator<ViewOrderDetailsQuery>
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public ViewOrderDetailsValidator(IOrderItemRepository orderItemRepository)
        {
            ApplyCustomValidationRules();
            _orderItemRepository = orderItemRepository;
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(vodq => vodq.orderid)
                .MustAsync(async (key, cancellation) =>
                {
                    return await _orderItemRepository.isOrderIdInOrderitemsAsync(key);
                }).WithMessage(SharedResponseKeys.NotFoundOrderId);

        }
    }
}
