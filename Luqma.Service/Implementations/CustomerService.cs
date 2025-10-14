using Luqma.Data.Entities;
using Luqma.Data.Enums;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;

namespace Luqma.Service.Implementations
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWhatsAppService _whatsAppService;

        public CustomerService(ICustomerRepository customerRepository, IWhatsAppService whatsAppService)
        {
            _customerRepository = customerRepository;
            _whatsAppService = whatsAppService;
        }
        /* public async Task<string> ConfirmPhoneNemberCode(string phoneNumber,string code)
         {

             var customer=_customerRepository.getByPhoneNumberAsync(phoneNumber);
             if(customer is null)
             {
                 return "the customer phonenumber is not found";
             }

         }*/
        public async Task<string> UpdateCustomerAsync(string phonenumber,string firstname,string lastname,Gender gender, string city, string state, string street)
        {
            var cust = await _customerRepository.IsPhoneNumberExistAsync(phonenumber);
                cust.FirstName = firstname;
                cust.LastName = lastname;
                cust.gender = gender;
    
                foreach (var c in cust.Addresses)
                {
                    if (c.City.Equals(city) && c.State.Equals(state) && c.Street.Equals(street))
                    {
                        await _customerRepository.SaveChangesAsync();
                       
                        return "the customer is updated suuccessfully";

                    }
                }
                cust.Addresses.Add(new CustomerAddress()
                {
                    City = city,
                    State = state,
                    Street = street
                });

                await _customerRepository.SaveChangesAsync();
               
              
                return "the customer is updated suuccessfully";
            }
          




        
        public async Task<string> ConfirmPhoneNumberCodeAsync(string phonenumber,string code)
        {
           var customer= await _customerRepository.getByPhoneNumberAsync(phonenumber);
            if (customer is null) return "the customer phonenumber is not found";
            if (!code.Equals(customer.Code)) return "the code is not correct";
            if (DateTime.UtcNow >= customer.ExpireDate) return "the code has expired";
            customer.Code = null;
            customer.ExpireDate = null;
            await _customerRepository.SaveChangesAsync();
            return "TheCodeHasbeenVerified";


        }
        public async Task<(Customer?,string)> AddPhoneNumberAsync(Customer customer)
        {
           var cust= await _customerRepository.IsPhoneNumberExistAsync(customer.PhoneNumber);

            if (cust != null)
            {
                var gud = Guid.NewGuid().ToByteArray();
                var cod = (Int32)(BitConverter.ToUInt32(gud, 0) % 900000) + 100000;
                cust.Code = cod.ToString();
                cust.ExpireDate = DateTime.UtcNow.AddMinutes(10);
                await _customerRepository.SaveChangesAsync();
                var whatAppResult = await _whatsAppService.SendPhoneNumberConfirmationCodeAsync(cust.PhoneNumber, cod.ToString());
                if (whatAppResult.Equals("Failed"))
                    return (null, "AnErrorOccurredWhileSendingTheNumberConfirmationCodeToYourPhone");
                return (cust, "the custmoer phonenumber is found");
              

            }
         
            var guid = Guid.NewGuid().ToByteArray();
            var code = (Int32)(BitConverter.ToUInt32(guid, 0) % 900000) + 100000;
            customer.Code = code.ToString();
            customer.ExpireDate = DateTime.UtcNow.AddMinutes(10);
            await _customerRepository.AddAsync(customer);
            var whatsAppResult = await _whatsAppService.SendPhoneNumberConfirmationCodeAsync(customer.PhoneNumber, code.ToString());
            if (whatsAppResult.Equals("Failed"))
                return (null,"AnErrorOccurredWhileSendingTheNumberConfirmationCodeToYourPhone");

            return (null,"the customer is added successfully");




        }
    }
}