using AutoMapper;
using Luqma.Core.Features.MenuItems.Commands.Models;
using Luqma.Data.Entities;
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
        }

    }
}
