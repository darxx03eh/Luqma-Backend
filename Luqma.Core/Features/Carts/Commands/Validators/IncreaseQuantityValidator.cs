using FluentValidation;
using Luqma.Core.Features.Carts.Commands.Models;
using Luqma.Core.ResponseKeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Carts.Commands.Validators
{
    public class IncreaseQuantityValidator : AbstractValidator<IncreaseQuantityCommand>
    {
        public IncreaseQuantityValidator()
        {


        }
    }
}
