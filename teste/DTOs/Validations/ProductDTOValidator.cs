using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(X => X.CodErp)
              .NotEmpty()
              .NotNull()
              .WithMessage("CodErp deve ser informado");

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name de ser informado");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price deve ser maior que zero");
        }
    }
}
