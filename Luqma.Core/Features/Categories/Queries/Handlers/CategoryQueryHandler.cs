using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Categories.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Response.Categories;
using Luqma.Service.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Luqma.Core.Features.Categories.Queries.Handlers
{
   public class CategoryQueryHandler:ApiResponseHandler,
        IRequestHandler<GetAllCategoryQuery,ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryQueryHandler(IMapper mapper,ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<ApiResponse> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
          var(categories,message)=  await _categoryService.GetAllAsync();
           var CategoriesResponse= _mapper.Map<ICollection<CategoryResponse>>(categories);
            return message switch
            {

                "sorry! the categories are not found" => NotFound(SharedResponseKeys.NotFound),
                "the categories are viewed successfully" => Success(CategoriesResponse, new { page = 1 },SharedResponseKeys.Success)
            };
            
            
        }
    }
}
