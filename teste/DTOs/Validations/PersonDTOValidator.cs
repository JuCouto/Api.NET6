using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class PersonDTOValidator : AbstractValidator<PersonDTO>
    {
        public PersonDTOValidator()
        {
            RuleFor(x => x.Document).NotEmpty().NotNull().WithMessage("Documento deve ser informado!");

            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name deve ser informado!");

            RuleFor(x => x.Phone).NotEmpty().NotNull().WithMessage("Phone deve ser informado!");
        }
        
    }
}
