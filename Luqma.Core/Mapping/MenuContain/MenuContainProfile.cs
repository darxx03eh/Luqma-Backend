using AutoMapper;
using Luqma.Data.Entities;
using Luqma.Data.Response.CategoryItems;
using Luqma.Data.Response.MenuContains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Mapping.MenuContain
{
   public class MenuContainProfile:Profile
    {
        public MenuContainProfile()
        {
            CreateMap<MenuContains,MenuContainsRespons>()
                .ForMember(mcr => mcr.Item, from => from.MapFrom(ci => ci.MenuItem.Item))
                .ForMember(mcr => mcr.Description, from => from.MapFrom(ci => ci.MenuItem.Description))
                .ForMember(mcr => mcr.Discount, from => from.MapFrom(ci => ci.MenuItem.Discount))
                .ForMember(mcr => mcr.Price, from => from.MapFrom(ci => ci.MenuItem.Price))
                .ForMember(mcr => mcr.IsVegetarian, from => from.MapFrom(ci => ci.MenuItem.IsVegetarian))
                .ForMember(mcr => mcr.ImageUrl, from => from.MapFrom(ci => ci.MenuItem.ImageUrl));

        }
    }
}
