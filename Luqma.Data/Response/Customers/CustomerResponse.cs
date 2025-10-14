using Luqma.Data.Entities;
using Luqma.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Luqma.Data.Response.Customers
{
   public class CustomerResponse
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender gender { get; set; }
       public ICollection<CustomerAddressResponse> Addresses { get; set; }
        public string PhoneNumber { get; set; }
    }
}
