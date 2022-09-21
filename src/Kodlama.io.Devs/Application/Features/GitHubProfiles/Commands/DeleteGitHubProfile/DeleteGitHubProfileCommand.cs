using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile
{
    public class DeleteGitHubProfileCommand:IRequest<DeletedGitHubProfileDto>
    {
        public string UserEmail { get; set; }
        public string ProfileName { get; set; }
        public class DeleteGitHubProfileCommandHandler:IRequestHandler<DeleteGitHubProfileCommand,DeletedGitHubProfileDto>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IDeveloperRepository _developerRepository;
            private readonly IMapper _mapper;
            private readonly GitHubProfileBusinessRules _gitHubProfileBusinessRules;

            public DeleteGitHubProfileCommandHandler(IGitHubProfileRepository gitHubProfileRepository, IDeveloperRepository developerRepository, IMapper mapper, GitHubProfileBusinessRules gitHubProfileBusinessRules)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _developerRepository = developerRepository;
                _mapper = mapper;
                _gitHubProfileBusinessRules = gitHubProfileBusinessRules;
            }

            public async Task<DeletedGitHubProfileDto> Handle(DeleteGitHubProfileCommand request, CancellationToken cancellationToken)
            {
                await _gitHubProfileBusinessRules.GitHubProfileExistCheck(request.UserEmail, request.ProfileName);
                Developer developer = await _developerRepository.GetAsync(c => c.Email == request.UserEmail);
                GitHubProfile profile = await _gitHubProfileRepository.GetAsync(c => c.ProfileName == request.ProfileName && c.DeveloperId == developer.Id);
                GitHubProfile deletedProfile = await _gitHubProfileRepository.DeleteAsync(profile);
                DeletedGitHubProfileDto deletedGitHubProfileDto = _mapper.Map<DeletedGitHubProfileDto>(deletedProfile);
                return deletedGitHubProfileDto;
            }
        }
    }
}
