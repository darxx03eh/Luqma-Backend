using Luqma.Data.Entities;
using Luqma.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Interfaces
{
    public interface ICustomerService
    {

        public Task<(Customer, string)> UpdateCustomerAsync(string phonenumber, string firstname, string lastname, Gender gender, string city, string state, string street);
        public Task<(string?, string)> ConfirmPhoneNumberCodeAsync(string phonenumber, string code);
        public Task<(Customer?, string)> AddPhoneNumberAsync(Customer customer);
    }
}
