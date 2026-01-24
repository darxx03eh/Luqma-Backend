using AutoMapper;
using Luqma.Data.Entities;
using Luqma.Data.Response.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Mapping.Orders
{
    public class OrderProfile : Profile
    {
       public OrderProfile()
        {
            CreateMap<Order, ViewOrderResponse>()
                .ForMember(vir => vir.Date, from => from.MapFrom(o => o.Date.ToString("MM/dd/yyyy hh:mm:ss tt")));
            CreateMap<OrderItem, ViewOrderDetailsResponse>()
                .ForMember(vodr => vodr.Item, from => from.MapFrom(oi => oi.MenuItem.Item));

                
        }
    }
}
