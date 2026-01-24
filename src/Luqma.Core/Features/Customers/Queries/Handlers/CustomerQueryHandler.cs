using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Customers.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities;
using Luqma.Data.Response.Customers;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Customers.Queries.Handlers
{
    public class CustomerQueryHandler : ApiResponseHandler,
        IRequestHandler<GetCustomerInformationQuery, ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerQueryHandler(IMapper mapper,ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }
        public async Task<ApiResponse> Handle(GetCustomerInformationQuery request, CancellationToken cancellationToken)
        {
           var (cust,result)= await _customerService.GetCustomerInfoAsync();
            var CustomerRe=_mapper.Map<CustomerResponse>(cust);
            return result switch
            {
                "the customer info is fetched successfully" => Success(CustomerRe, message: SharedResponseKeys.SuccessFetchedCustmoerInfo)
            };



        }
    }
}
