using Luqma.Data.Entities;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly LuqmaDbContext _context;

        public CustomerRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }

        public async Task<Customer?> getByPhoneNumberAsync(string phonenumber)
        {

            return _context.Customers.FirstOrDefault(c => c.PhoneNumber == phonenumber);


        }
        public async Task<Customer?> IsPhoneNumberExistAsync(string phonenumber)
        {
           var customer= _context.Customers.FirstOrDefault(c => c.PhoneNumber == phonenumber);
            return customer;
        }




    }
}

