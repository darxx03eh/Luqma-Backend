using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Customers.commands.Models;
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

namespace Luqma.Core.Features.Customers.commands.Handlers
{
    public class CustomerCommandHandler : ApiResponseHandler,
       
        IRequestHandler<UpdateCustomerDetailsCommand,ApiResponse>,
        IRequestHandler<ConfirmPhoneNumberCodeCommand,ApiResponse>,
        IRequestHandler<AddPhoneNumberCommand,ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerCommandHandler(IMapper mapper,ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }
    

        public async Task<ApiResponse> Handle(UpdateCustomerDetailsCommand request, CancellationToken cancellationToken)
        {
            var customer=_mapper.Map<Customer>(request);
          var( cust,result)= await  _customerService.UpdateCustomerAsync(request.PhoneNumber,request.FirstName,request.LastName,request.gender, request.City, request.State, request.Street);
            return result switch {
                "the customer is updated suuccessfully" => Success(cust, message:SharedResponseKeys.SuccessUpdateCutomer),
               
            };
        }

        public async Task<ApiResponse> Handle(ConfirmPhoneNumberCodeCommand request, CancellationToken cancellationToken)
        {
           var (token,result)= await _customerService.ConfirmPhoneNumberCodeAsync(request.PhoneNumber, request.Code);
            return result switch
            {
                "the customer phonenumber is not found" => NotFound(SharedResponseKeys.NotFoundCustomer),
                "the code is not correct" => BadRequest(SharedResponseKeys.TheCodeEnteredIsIncorrect),
                "the code has expired" => BadRequest(SharedResponseKeys.TheCodeHasExpired),
                "TheCodeHasbeenVerified" => Success(token,message:SharedResponseKeys.TheCodeHasbeenVerified)
            };
        }

        public async Task<ApiResponse> Handle(AddPhoneNumberCommand request, CancellationToken cancellationToken)
        {
           var customer =_mapper.Map<Customer>(request);
          var(result,messgae)= await  _customerService.AddPhoneNumberAsync(customer);
           var customerRe= _mapper.Map<CustomerResponse>(result);
            return messgae switch
            {
                "AnErrorOccurredWhileSendingTheNumberConfirmationCodeToYourPhone" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSendingTheNumberConfirmationCodeToYourPhone),
                "the custmoer phonenumber is found" => Success(customerRe, message: SharedResponseKeys.FoundCustomerPhonenumber),
                "the customer is added successfully" => Success(null, message:SharedResponseKeys.SuccessAddCustomer)
            };
        }
    }
}
