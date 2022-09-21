using Application.Features.GitHubProfiles.Commands.AddGitHubProfile;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Commands.CreateGitHubProfile
{
    public class CreateGitHubProfileCommandValidator:AbstractValidator<CreateGitHubProfileCommand>
    {
        public CreateGitHubProfileCommandValidator()
        {
            RuleFor(c => c.UserEmail).NotEmpty();
            RuleFor(c => c.ProfileUrl).NotEmpty();
            RuleFor(c => c.ProfileName).NotEmpty();
        }
    }
}
