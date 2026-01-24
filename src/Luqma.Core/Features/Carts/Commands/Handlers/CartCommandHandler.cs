using Luqma.Core.Bases;
using Luqma.Core.Features.Carts.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Carts.Commands.Handlers
{
    public class CartCommandHandler : ApiResponseHandler,

         IRequestHandler<AddToCartCommand, ApiResponse>,
        IRequestHandler<IncreaseQuantityCommand, ApiResponse>,
        IRequestHandler<DecreaseQuantityCommand, ApiResponse>,
        IRequestHandler<DeleteItemFromCartForCustomer,ApiResponse>

    {
        private readonly ICartService _cartService;

        public CartCommandHandler(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<ApiResponse> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _cartService.AddToCartAsync(request.ItemId);
            return result switch
            {
                "Error while extract userid frim token" => InternalServerError(SharedResponseKeys.ErrorExtractUseridfromToken),
                "the item is added to cart by customer successfully" => Success(null, message: SharedResponseKeys.SuccessAddCartFromCustomer)
            };

        }

        public async Task<ApiResponse> Handle(IncreaseQuantityCommand request, CancellationToken cancellationToken)
        {
          var result= await  _cartService.IncreaseQuantityAsync(request.ItemId);
            return result switch
            {
                "Error while extract userid from token" => InternalServerError(SharedResponseKeys.ErrorExtractUseridfromToken),
                "the cart is not found" => NotFound(SharedResponseKeys.NotFoundCart),
                "the quantity is increased succsessfully" => Success(null, message: SharedResponseKeys.SuccessIncreaseQuantity),
                "the quantity is not increased" => InternalServerError(SharedResponseKeys.FailIncreaseQuantity)
            };

        }

        public async Task<ApiResponse> Handle(DecreaseQuantityCommand request, CancellationToken cancellationToken)
        {
            var result = await _cartService.DecreaseQuantityAsync(request.ItemId);
            return result switch
            {
                "Error while extract userid from token" => InternalServerError(SharedResponseKeys.ErrorExtractUseridfromToken),
                "the cart is not found" => NotFound(SharedResponseKeys.NotFoundCart),
                "the quantity is decreased succsessfully" => Success(null, message: SharedResponseKeys.SuccessDecreaseQuantity),
                "the quantity is not decreased" => InternalServerError(SharedResponseKeys.FailDecreaseQuantity)
            };
        }

        public async Task<ApiResponse> Handle(DeleteItemFromCartForCustomer request, CancellationToken cancellationToken)
        {
           var result= await _cartService.DeleteItemFromCartForCustomerAsync(request.Id);
            return result switch
            {
                "Error while extract userid from token" => InternalServerError(SharedResponseKeys.ErrorExtractUseridfromToken),
                "the cart is not found" => NotFound(SharedResponseKeys.NotFoundCart),
                "the item is deleted from cart for customer successfully" => Deleted(SharedResponseKeys.SuccessDeleteItemFromCartForCustomer),
                "the item is not deleted from cart for customer" => InternalServerError(SharedResponseKeys.FailDeleteItemFromCartForCustomer)


            };
    }
    }
}
