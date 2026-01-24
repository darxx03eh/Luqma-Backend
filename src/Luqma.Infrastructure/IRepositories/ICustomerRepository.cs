using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.IRepositories
{
  public  interface ICustomerRepository :IGenericRepository<Customer>
    {

        public Task<Customer> getByPhoneNumberAsync(string phonenumber);
        public  Task<Customer?> IsPhoneNumberExistAsync(string phonenumber);

    }
}
