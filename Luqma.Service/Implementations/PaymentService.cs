using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Stripe.Checkout;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentService(IPaymentRepository paymentRepository, ICartRepository cartRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _paymentRepository = paymentRepository;
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> ProcessPaymentAsync(int OrderId, string PaymentMethod)
        {
            var customerid = int.Parse(_paymentRepository.ExtractUserIdFromToken());
            IEnumerable<Cart> carts =await _cartRepository.GetCartForCustomerAsync(customerid);
            if (carts is null) return "the cart is empty";
            var Request = _httpContextAccessor.HttpContext.Request;
            if (PaymentMethod.Equals("visa"))
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    Mode = "payment",//payment or Refund
                    LineItems = new List<SessionLineItemOptions> { },
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/api/Customer/CheckOuts/success/{OrderId}",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
                };
                foreach (var item in carts)
                {
                    options.LineItems.Add(new SessionLineItemOptions()
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description,
                            },
                            UnitAmount = (long)item.Product.Price,
                        },
                        Quantity = item.Count,
                    });
                }
                var service = new SessionService();
                var session = service.Create(options);
            }


        }
    }
}

