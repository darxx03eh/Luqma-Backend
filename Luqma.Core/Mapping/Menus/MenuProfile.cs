using AutoMapper;
using Luqma.Core.Features.Menus.Commands.Models;
using Luqma.Core.Features.Menus.Commands.Models;
using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Mapping.Menus
{
    public class MenuProfile:Profile
    {
        public MenuProfile()
        {
            CreateMap<AddMenuCommand,Menu>();
        }
    }
}
