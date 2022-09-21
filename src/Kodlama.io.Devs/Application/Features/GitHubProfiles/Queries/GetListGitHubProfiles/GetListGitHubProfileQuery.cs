using Application.Features.GitHubProfiles.Models;
using Application.Features.GitHubProfiles.Rules;
using Application.Services;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Queries.GetListGitHubProfiles
{
    public class GetListGitHubProfileQuery:IRequest<GitHubProfileListModel>
    {
        public string UserEmail { get; set; }
        public PageRequest PageRequest { get; set; }
        public class GetListGitHubProfileQueryHandler:IRequestHandler<GetListGitHubProfileQuery,GitHubProfileListModel>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IDeveloperRepository _developerRepository;
            private readonly IMapper _mapper;
            private readonly GitHubProfileBusinessRules _gitHubProfileBusinessRules;

            public GetListGitHubProfileQueryHandler(IGitHubProfileRepository gitHubProfileRepository, IDeveloperRepository developerRepository, IMapper mapper, GitHubProfileBusinessRules gitHubProfileBusinessRules)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _developerRepository = developerRepository;
                _mapper = mapper;
                _gitHubProfileBusinessRules = gitHubProfileBusinessRules;
            }

            public async Task<GitHubProfileListModel> Handle(GetListGitHubProfileQuery request, CancellationToken cancellationToken)
            {
                Developer developer = await _developerRepository.GetAsync(c => c.Email == request.UserEmail);
                IPaginate<GitHubProfile> profiles = await _gitHubProfileRepository.GetListAsync(c => c.DeveloperId == developer.Id,index:request.PageRequest.Page,size:request.PageRequest.PageSize);
                GitHubProfileListModel gitHubProfileListModel = _mapper.Map<GitHubProfileListModel>(profiles);
                return gitHubProfileListModel;
            }
        }
    }
}
