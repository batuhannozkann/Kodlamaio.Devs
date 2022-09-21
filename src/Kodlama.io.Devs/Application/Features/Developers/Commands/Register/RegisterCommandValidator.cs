using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.Register
{
    public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty().MinimumLength(7);
            RuleFor(c => c.Password).NotEmpty().MinimumLength(4);
            RuleFor(c => c.FirstName).NotEmpty().MinimumLength(3);
            RuleFor(c => c.LastName).NotEmpty().MinimumLength(2);
        }
    }
}
