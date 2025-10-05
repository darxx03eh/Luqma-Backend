using AutoMapper;
using Luqma.Data.Entities;
using Luqma.Data.Response.CategoryItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Mapping.CategoryItems
{
   public class CategoryItemProfile:Profile
    {
        public CategoryItemProfile()
        {
            CreateMap<CategoryItem, CategoryItemResponse>()
                .ForMember(cir => cir.Item, from => from.MapFrom(ci => ci.MenuItem.Item))
                .ForMember(cir => cir.Description, from => from.MapFrom(ci => ci.MenuItem.Description))
                .ForMember(cir => cir.Discount, from => from.MapFrom(ci => ci.MenuItem.Discount))
                .ForMember(cir => cir.Price, from => from.MapFrom(ci => ci.MenuItem.Price))
                .ForMember(cir => cir.IsVegetarian, from => from.MapFrom(ci => ci.MenuItem.IsVegetarian))
                .ForMember(cir => cir.ImageUrl, from => from.MapFrom(ci => ci.MenuItem.ImageUrl));
              
        }

    }
}
