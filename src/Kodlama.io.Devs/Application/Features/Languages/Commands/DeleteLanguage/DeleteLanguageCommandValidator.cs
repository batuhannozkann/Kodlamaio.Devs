using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommandValidator
    {
        public class DeleteByIdLanguageCommandValidator : AbstractValidator<DeleteByIdLanguageCommand>
        {
            public DeleteByIdLanguageCommandValidator()
            {
                RuleFor(b => b.Id).NotEmpty();
            }
        }
        public class DeleteByNameLanguageCommandValidator : AbstractValidator<DeleteByNameLanguageCommand>
        {
            public DeleteByNameLanguageCommandValidator()
            {
                RuleFor(b => b.Name).NotEmpty();
            }
        }
    }
}
