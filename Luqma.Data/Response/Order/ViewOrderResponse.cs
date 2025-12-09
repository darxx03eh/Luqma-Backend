using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Data.Response.Order
{
    public class ViewOrderResponse
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public double TotalPrice { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }

    }
}
