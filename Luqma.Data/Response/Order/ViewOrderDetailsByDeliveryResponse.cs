using Luqma.Data.Response.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Data.Response.Order
{
    public class ViewOrderDetailsByDeliveryResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string PhoneNumber { get; set; }
        public CustomerAddressResponse Address { get; set; }
        public string Status { get; set; }

    }
}
