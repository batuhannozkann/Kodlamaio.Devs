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

namespace Application.Features.GitHubProfiles.Commands.AddGitHubProfile
{
    public class CreateGitHubProfileCommand:IRequest<CreatedGitHubProfileDto>
    {
        public string UserEmail { get; set; }
        public string ProfileName { get; set; }
        public string ProfileUrl { get; set; }
        public class CreateGitHubProfileCommandHandler:IRequestHandler<CreateGitHubProfileCommand,CreatedGitHubProfileDto>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IDeveloperRepository _developerRepository;
            private readonly IMapper _mapper;
            private readonly GitHubProfileBusinessRules _gitHubProfileBusinessRules;

            public CreateGitHubProfileCommandHandler(IGitHubProfileRepository gitHubProfileRepository, IMapper mapper, GitHubProfileBusinessRules gitHubProfileBusinessRules, IDeveloperRepository developerRepository)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _mapper = mapper;
                _gitHubProfileBusinessRules = gitHubProfileBusinessRules;
                _developerRepository = developerRepository;
            }

            public async Task<CreatedGitHubProfileDto> Handle(CreateGitHubProfileCommand request, CancellationToken cancellationToken)
            {
                await _gitHubProfileBusinessRules.UserExistCheckForAddProfile(request.UserEmail);
                Developer developer = await _developerRepository.GetAsync(c => c.Email == request.UserEmail);
                await _gitHubProfileBusinessRules.GitHubProfileNameNotDuplicated(request.ProfileName, developer.Id);
                GitHubProfile profile = _mapper.Map<GitHubProfile>(request);
                profile.DeveloperId = developer.Id;
                GitHubProfile createdProfile = await _gitHubProfileRepository.AddAsync(profile);
                CreatedGitHubProfileDto createdGitHubProfileDto = _mapper.Map<CreatedGitHubProfileDto>(createdProfile);
                return createdGitHubProfileDto;
                
            }
        }
    }
}
