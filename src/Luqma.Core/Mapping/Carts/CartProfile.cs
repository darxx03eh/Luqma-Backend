using AutoMapper;
using Luqma.Data.Entities;
using Luqma.Data.Response.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Mapping.Carts
{
   public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartResponse>()
                .ForMember(cr => cr.Item, from => from.MapFrom(c => c.MenuItem.Item))
                .ForMember(cr => cr.Description, from => from.MapFrom(c => c.MenuItem.Description))
                .ForMember(cr => cr.Price, from => from.MapFrom(c => c.MenuItem.Price))
                .ForMember(cr => cr.ImageUrl, from => from.MapFrom(c => c.MenuItem.ImageUrl))
                .ForMember(cr => cr.Discount, from => from.MapFrom(c => Math.Round(c.MenuItem.Discount,2)))
                ;



        }

    }
}
