using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Categories.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Categories.Commands.Handlers
{
   public  class CategoryCommandHandler:ApiResponseHandler,IRequestHandler<AddCategoryCommand,ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryCommandHandler(IMapper mapper,ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<ApiResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
           var category= _mapper.Map<Category>(request);
          var result= await  _categoryService.CreateAsync(category);
            return result switch
            {
                "the category is created successfully" => Created(request,message: SharedResponseKeys.Success)
            };
        }
    }
}
