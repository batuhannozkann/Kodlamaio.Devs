using Application.Features.Technologies.Models;
using Application.Features.Technologies.Rules;
using Application.Services;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetListTechnology
{
    public class GetListTechnologyQuery : IRequest<ListTechnologyModel>
    {
        public PageRequest PageRequest;

        public class GetListTechnologyQueryHandler : IRequestHandler<GetListTechnologyQuery, ListTechnologyModel>
        {
            private ITechnologyRepository _technologyRepository;
            private IMapper _mapper;
            private TechnologyBusinessRules _technologyBusinessRules;

            public GetListTechnologyQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<ListTechnologyModel> Handle(GetListTechnologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> technologies = await _technologyRepository.GetListAsync(include: m => m.Include(b => b.Language)
                , index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
                );
                ListTechnologyModel mappedTechnology = _mapper.Map<ListTechnologyModel>(technologies);
                return mappedTechnology;
            }
        }
    }
}
