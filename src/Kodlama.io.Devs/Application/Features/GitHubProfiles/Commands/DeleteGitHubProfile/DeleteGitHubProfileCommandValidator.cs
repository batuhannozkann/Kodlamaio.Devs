using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile
{
    public class DeleteGitHubProfileCommandValidator:AbstractValidator<DeleteGitHubProfileCommand>
    {
        public DeleteGitHubProfileCommandValidator()
        {
            RuleFor(c => c.UserEmail).NotEmpty();
            RuleFor(c => c.ProfileName).NotEmpty();
        }
    }
}
