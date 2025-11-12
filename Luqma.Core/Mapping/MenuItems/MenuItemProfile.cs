using AutoMapper;
using Luqma.Core.Features.MenuItems.Commands.Models;
using Luqma.Data.Entities;
using Luqma.Data.Response.MenuItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Mapping.MenuItems
{
    public class MenuItemProfile:Profile
    {
        public MenuItemProfile()
        {
            CreateMap<AddMenuItemCommand, MenuItem>();
            CreateMap<DeleteMenuItemCommand, MenuItem>();
            CreateMap<UpdateMenuItemCommand, MenuItem>();
            CreateMap<MenuItem, MenuItemResponse>()
                .ForMember(mir => mir.ItemId, from => from.MapFrom(m =>m.Id))

                .ForMember(mir => mir.CategoryId, from => from.MapFrom(m => m.CategoryItems.FirstOrDefault(ci => ci.ItemId == m.Id).CategoryId))
                .ForMember(mir => mir.Title, from => from.MapFrom(m => m.CategoryItems.First(ci => ci.ItemId == m.Id).Category.Title));
                
                
           

        }

    }
}
