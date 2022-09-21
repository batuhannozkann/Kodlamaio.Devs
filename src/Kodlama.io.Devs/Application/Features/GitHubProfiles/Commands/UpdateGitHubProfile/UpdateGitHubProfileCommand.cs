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

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile
{
    public class UpdateGitHubProfileCommand:IRequest<UpdatedGitHubProfileDto>
    {
        public int DeveloperId { get; set; }
        public int Id { get; set; }
        public string ProfileName { get; set; }
        public string? ProfileUrl { get; set; }
        public class UpdateGitHubProfileCommandHandler:IRequestHandler<UpdateGitHubProfileCommand,UpdatedGitHubProfileDto>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IDeveloperRepository _developerRepository;
            private readonly IMapper _mapper;
            private readonly GitHubProfileBusinessRules _gitHubProfileBusinessRules;

            public UpdateGitHubProfileCommandHandler(IGitHubProfileRepository gitHubProfileRepository, IDeveloperRepository developerRepository, IMapper mapper, GitHubProfileBusinessRules gitHubProfileBusinessRules)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _developerRepository = developerRepository;
                _mapper = mapper;
                _gitHubProfileBusinessRules = gitHubProfileBusinessRules;
            }

            public async Task<UpdatedGitHubProfileDto> Handle(UpdateGitHubProfileCommand request, CancellationToken cancellationToken)
            {
                await _gitHubProfileBusinessRules.GitHubProfileExistCheck(request.Id, request.DeveloperId);
                GitHubProfile mappedProfile = _mapper.Map<GitHubProfile>(request);
                await _gitHubProfileBusinessRules.GitHubProfileNameNotDuplicated(request.ProfileName, request.DeveloperId);
                GitHubProfile updatedProfile = await _gitHubProfileRepository.UpdateAsync(mappedProfile);
                UpdatedGitHubProfileDto updatedGitHubProfileDto = _mapper.Map<UpdatedGitHubProfileDto>(updatedProfile);
                return updatedGitHubProfileDto;
            }
        }
    }
}
