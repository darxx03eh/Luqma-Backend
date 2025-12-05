using Luqma.Data.Entities;
using Luqma.Data.Routing;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Stripe;
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
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public PaymentService(IPaymentRepository paymentRepository, ICartRepository cartRepository,
            IHttpContextAccessor httpContextAccessor,IOrderRepository orderRepository,IOrderItemRepository orderItemRepository)
        {
            _paymentRepository = paymentRepository;
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }
        public async Task<string> SuccessPaymentByVisaAsync(int orderid)
        {
            var listoforderitem = new List<OrderItem>();
            var order = await _orderRepository.GetByIdAsync(orderid);
            var payment = new Payment()
            {
                Currency = "USD",
                PaymentMethod = "visa",
                Status = "paid",
                Amount = order.TotalPrice
            };
            payment.PaymentsOrders.Add(new PaymentsOrder()
            {
                OrderId = orderid
            });
            await _paymentRepository.AddAsync(payment);
            var cartsforcustomer = await _cartRepository.GetCartForCustomerAsync(order.CustomerId);
            foreach (var c in cartsforcustomer)
            {
                var orderitem = new OrderItem()
                {
                    OrderId = orderid,
                    ItemId = c.ItemId,
                    Quantity = c.Quantity,
                    TotalPrice = Math.Round((c.MenuItem.Price - (c.MenuItem.Discount * c.MenuItem.Price)) * c.Quantity)

                };
                listoforderitem.Add(orderitem);
            }
                await _orderItemRepository.AddRangeAsync(listoforderitem);
                await _cartRepository.ClearCartAsync(order.CustomerId);
                return "the payment by visa is success";
        }
        public async Task<(string?,string)> ProcessPaymentAsync(int OrderId,string paymentMethod)
        {
             var order=await _orderRepository.GetByIdAsync(OrderId);
            var customerid = int.Parse(_paymentRepository.ExtractUserIdFromToken());

            IEnumerable<Cart> carts =await _cartRepository.GetCartForCustomerAsync(customerid);
            if (!carts.Any()) return (null,"the cart is empty");

            var Request = _httpContextAccessor.HttpContext.Request;
            if (paymentMethod.Equals("visa")){

            
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    Mode = "payment",//payment or Refund
                    LineItems = new List<SessionLineItemOptions> { },
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/{Router.CustomerPaymentRouting.SuccessPayment}?orderid={OrderId}",
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
                                Name = item.MenuItem.Item,
                                Description = item.MenuItem.Description,

                            },
                            UnitAmount =(long)Math.Round((decimal)((item.MenuItem.Price-(item.MenuItem.Price*item.MenuItem.Discount)) * 100))
                            
                        },
                        Quantity = (long)item.Quantity,
                       
                    });
                }
                var service = new SessionService();
                var session = service.Create(options);
                return (session.Url, "the process payment by visa is done");
            }
            if ("cash".Equals(paymentMethod))
            {
                var listoforderitem = new List<OrderItem>();
                var payment = new Payment()
            {
                Currency = "USD",
                PaymentMethod = "cash",
                Status = "UnPaid",
                Amount = order.TotalPrice
            };
            payment.PaymentsOrders.Add(new PaymentsOrder()
            {
                OrderId = OrderId
            });
            await _paymentRepository.AddAsync(payment);
                var cartsforcustomer = await _cartRepository.GetCartForCustomerAsync(order.CustomerId);
                foreach (var c in cartsforcustomer)
                {
                    var orderitem = new OrderItem()
                    {
                        OrderId = OrderId,
                        ItemId = c.ItemId,
                        Quantity = c.Quantity,
                        TotalPrice = Math.Round((c.MenuItem.Price - (c.MenuItem.Discount * c.MenuItem.Price)) * c.Quantity)



                    };
                    listoforderitem.Add(orderitem);
                }
                    await _orderItemRepository.AddRangeAsync(listoforderitem);
                    await _cartRepository.ClearCartAsync(order.CustomerId);

                    return (null, "the process payment by cash is done");
            }

            return (null, "the payment method is not exist in our website");
        }
    }
}

