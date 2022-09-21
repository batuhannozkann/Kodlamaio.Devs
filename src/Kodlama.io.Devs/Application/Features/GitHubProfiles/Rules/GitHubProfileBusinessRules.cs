using Application.Services;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Rules
{
    public class GitHubProfileBusinessRules
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IGitHubProfileRepository _gitHubProfileRepository;

        public GitHubProfileBusinessRules(IDeveloperRepository developerRepository, IGitHubProfileRepository gitHubProfileRepository)
        {
            _developerRepository = developerRepository;
            _gitHubProfileRepository = gitHubProfileRepository;
        }
        public async Task UserExistCheckForAddProfile(string email)
        {
            var user = await _developerRepository.GetAsync(c => c.Email == email);
            if (user == null) throw new BusinessException("User does not exist in the system");
        }
        public async Task GitHubProfileNameNotDuplicated(string profilename,int developerid)
        {
            var user = await _gitHubProfileRepository.GetAsync(c => c.ProfileName == profilename && c.DeveloperId == developerid);
            if (user != null) throw new BusinessException("Profile name is exist");
        }
        public async Task GitHubProfileExistCheck(int id ,int developerid)
        {
            GitHubProfile? profile = await _gitHubProfileRepository.GetAsync(c => c.Id == id && c.DeveloperId == developerid);
            if (profile == null) throw new BusinessException("Profile does not exist");
        }
        public async Task GitHubProfileExistCheck(string UserEmail, string ProfileName)
        {
            GitHubProfile? profile = await _gitHubProfileRepository.GetAsync(c => c.Developer.Email == UserEmail && c.ProfileName == ProfileName);
            if (profile == null) throw new BusinessException("Profile does not exist");
        }
    }
}
