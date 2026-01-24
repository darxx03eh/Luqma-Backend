using AutoMapper;
using Luqma.Core.Features.Customers.commands.Models;
using Luqma.Data.Entities;
using Luqma.Data.Response.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Mapping.Customers
{
   public class CustomerProfile :Profile
    {

        public CustomerProfile()
        {
            CreateMap<UpdateCustomerDetailsCommand, Customer>();
            CreateMap<AddPhoneNumberCommand, Customer>();
            CreateMap<Customer, CustomerResponse>()
                .ForMember(cr => cr.Addresses, from => from.MapFrom(c => c.Addresses.Select(c => new CustomerAddressResponse()
                {
                    City = c.City,
                    State = c.State,
                    Street = c.Street
                })));
               
        }
    }
}
