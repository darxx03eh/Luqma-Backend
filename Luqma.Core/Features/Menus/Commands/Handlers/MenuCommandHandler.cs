using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Menus.Commands.Models;
using Luqma.Data.Entities;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luqma.Data.Entities;
using Luqma.Core.Features.Menus.Commands.Models;
using Luqma.Core.ResponseKeys;

namespace Luqma.Core.Features.Menus.Commands.Handlers
{
    public class MenuCommandHandler : ApiResponseHandler,
         IRequestHandler<AddMenuCommand, ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;

        public MenuCommandHandler(IMapper mapper,IMenuService menuService)
        {
            _mapper = mapper;
            _menuService = menuService;
        }
        public async Task<ApiResponse> Handle(AddMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = _mapper.Map<Menu>(request);
           var result= await _menuService.AddMenuAsync(menu);
            return result switch
            {
                "the menu is added successfully" => Created(null, message: SharedResponseKeys.SuccessAddMenu),
                "the menu is null" => InternalServerError(SharedResponseKeys.NullMenu),
                _ => InternalServerError(SharedResponseKeys.AnErrorWhileAddMenu)
            };


            
        }
    }
}
