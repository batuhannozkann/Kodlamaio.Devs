using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateTechnologyCommand>
    {
        public UpdateTechnologyCommandValidator()
        {
            RuleFor(b => b.Id).NotEmpty();
            RuleFor(b => b.LanguageId).NotEmpty();
            RuleFor(b => b.Name).NotEmpty();        
        }
    }
}
