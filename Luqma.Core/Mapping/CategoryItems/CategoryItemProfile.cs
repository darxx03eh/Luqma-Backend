using AutoMapper;
using Luqma.Data.Entities;
using Luqma.Data.Response.CategoryItems;
using Luqma.Data.Response.MenuItems;
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
                .ForMember(cir => cir.ImageUrl, from => from.MapFrom(ci => ci.MenuItem.ImageUrl))
                .ForMember(cir => cir.Status, from => from.MapFrom(ci => ci.MenuItem.Status));


            CreateMap<CategoryItem, MenuItemResponse>()
                 .ForMember(mir => mir.Item, from => from.MapFrom(ci => ci.MenuItem.Item))
                .ForMember(mir => mir.Description, from => from.MapFrom(ci => ci.MenuItem.Description))
                .ForMember(mir => mir.Discount, from => from.MapFrom(ci => ci.MenuItem.Discount))
                .ForMember(mir => mir.Price, from => from.MapFrom(ci => ci.MenuItem.Price))
                .ForMember(mir => mir.IsVegetarian, from => from.MapFrom(ci => ci.MenuItem.IsVegetarian))
                .ForMember(mir => mir.ImageUrl, from => from.MapFrom(ci => ci.MenuItem.ImageUrl))
                .ForMember(mir => mir.Status, from => from.MapFrom(ci => ci.MenuItem.Status))
                .ForMember(mir => mir.Title, from => from.MapFrom(ci => ci.Category.Title));





        }

    }
}
