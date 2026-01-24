using Luqma.Core.Bases;
using Luqma.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.MenuItems.Commands.Models
{
    public class UpdateMenuItemCommand:IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public IFormFile? Image { get; set; }
        public string? Description { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public bool? IsVegetarian { get; set; }
        public int CategoryId { get; set; }
       




    }
}
