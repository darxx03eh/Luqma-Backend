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
        IRequestHandler<GetAllCategoryQuery,ApiResponse>,
        IRequestHandler<GetCategoryQuery,ApiResponse>
       
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

        public async Task<ApiResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
          var(category,message)= await  _categoryService.GetByIdAsync(request.Id);
            var categoryResponse = _mapper.Map<CategoryResponse>(category);
            return message switch
            {
                "the category is not found" => NotFound(SharedResponseKeys.CategoryNotFound),
                "the category is fetched successfully" => Success(categoryResponse, message: SharedResponseKeys.SuccessGetCategory),
                _ => InternalServerError(SharedResponseKeys.AnErrorWhileFetchCategory)
            };
        }
    }
}
