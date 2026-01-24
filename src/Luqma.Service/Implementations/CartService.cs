using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Implementations
{
   public class CartService :ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<string> AddToCartAsync(int itemid)
        {
            var customerid = _cartRepository.ExtractUserIdFromToken();
            if (customerid is null) return "Error while extract userid from token";
            var cart = new Cart()
            {
                CustomerId = int.Parse(customerid),
                ItemId = itemid,
                Quantity = 1

            };
            await _cartRepository.AddAsync(cart);
            return "the item is added to cart by customer successfully";
        }
        public async Task<(IQueryable<Cart>?,string)> GetCartForCustomerAsync()
        {
            var customerid = _cartRepository.ExtractUserIdFromToken();
            if (customerid is null) return (null,"Error while extract userid from token");

          var carts= await  _cartRepository.GetCartForCustomerAsync(int.Parse(customerid));
            return (carts, "the cart for customer is fetched successfully");
        }
        public async Task<string> IncreaseQuantityAsync(int itemId)
        {
            var customerid = _cartRepository.ExtractUserIdFromToken();
            if (customerid is null) return "Error while extract userid from token";
            var cart = await _cartRepository.GetByItemIdAndCustomerIdAsync(itemId, int.Parse(customerid));
            if (cart is null) return "the cart is not found";
            cart.Quantity++;
          var count= await  _cartRepository.UpdateAsync(cart);
            return count > 0 ? "the quantity is increased succsessfully" : "the quantity is not increased";

        }
        public async Task<string> DecreaseQuantityAsync(int itemId)
        {
            var customerid = _cartRepository.ExtractUserIdFromToken();
            if (customerid is null) return "Error while extract userid from token";
            var cart = await _cartRepository.GetByItemIdAndCustomerIdAsync(itemId, int.Parse(customerid));
            if (cart is null) return "the cart is not found";
            cart.Quantity--;
            var count = await _cartRepository.UpdateAsync(cart);
            return count > 0 ? "the quantity is decreased succsessfully" : "the quantity is not decreased";

        }
        public async Task<string>DeleteItemFromCartForCustomerAsync(int itemid)
        {
            var customerid = _cartRepository.ExtractUserIdFromToken();
            if (customerid is null) return "Error while extract userid from token";
            var cart = await _cartRepository.GetByItemIdAndCustomerIdAsync(itemid, int.Parse(customerid));
            if (cart is null) return "the cart is not found";
            var count= await _cartRepository.DeleteAsync(cart);
            return count > 0 ? "the item is deleted from cart for customer successfully" : "the item is not deleted from cart for customer";

        }


    }
}
