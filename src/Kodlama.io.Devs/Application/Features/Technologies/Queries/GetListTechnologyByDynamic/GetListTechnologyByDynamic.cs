using Application.Features.Technologies.Models;
using Application.Features.Technologies.Rules;
using Application.Services;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetListTechnologyByDynamic
{
    public class GetListTechnologyByDynamicQuery:IRequest<ListTechnologyModel>
    {
        public PageRequest PageRequest { get; set; }
        public Dynamic Dynamic { get; set; }
        public class GetListTechnologyByDynamicQueryHandler : IRequestHandler<GetListTechnologyByDynamicQuery, ListTechnologyModel>
        {
            private ITechnologyRepository _technologyRepository;
            private IMapper _mapper;
            private TechnologyBusinessRules _technologyBusinessRules;

            public GetListTechnologyByDynamicQueryHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<ListTechnologyModel> Handle(GetListTechnologyByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> technologies = await _technologyRepository.GetListByDynamicAsync(request.Dynamic, include: m => m.Include(b => b.Language),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                ListTechnologyModel technologyModel = _mapper.Map<ListTechnologyModel>(technologies);
                return technologyModel;
            }
        }
    }
}
