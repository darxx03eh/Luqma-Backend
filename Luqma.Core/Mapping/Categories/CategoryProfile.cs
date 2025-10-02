using AutoMapper;
using Luqma.Core.Features.Categories.Commands.Models;
using Luqma.Data.Entities;
using Luqma.Data.Response.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Mapping.Categories
{
   public partial class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<AddCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<DeleteCategoryCommand, Category>();

        }
    }
}
