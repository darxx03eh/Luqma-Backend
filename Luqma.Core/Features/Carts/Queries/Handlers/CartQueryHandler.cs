using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Carts.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Response.Carts;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Carts.Queries.Handlers
{
    public class CartQueryHandler : ApiResponseHandler,
         IRequestHandler<GetCartforCustomer, ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;

        public CartQueryHandler(IMapper mapper,ICartService cartService)
        {
            _mapper = mapper;
            _cartService = cartService;
        }
        public async Task<ApiResponse> Handle(GetCartforCustomer request, CancellationToken cancellationToken)
        {
           var (carts,result)= await _cartService.GetCartForCustomerAsync();
           var cartRe= _mapper.Map<List<CartResponse>>(carts);
            return result switch
            {
                "Error while extract userid from token" => InternalServerError(SharedResponseKeys.ErrorExtractUseridfromToken),
                "the cart for customer is fetched successfully" => Success(cartRe, message: SharedResponseKeys.SuccessGetCartForCustomer)
            };
         
        }
    }
}
