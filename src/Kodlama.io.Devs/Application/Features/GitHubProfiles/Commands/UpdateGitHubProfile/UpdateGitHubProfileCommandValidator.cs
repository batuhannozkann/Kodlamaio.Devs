using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile
{
    public class UpdateGitHubProfileCommandValidator:AbstractValidator<UpdateGitHubProfileCommand>
    {
        public UpdateGitHubProfileCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.DeveloperId).NotEmpty();
            RuleFor(c => c.ProfileUrl).NotEmpty();
            RuleFor(c => c.ProfileName).NotEmpty();
        }
    }
}
