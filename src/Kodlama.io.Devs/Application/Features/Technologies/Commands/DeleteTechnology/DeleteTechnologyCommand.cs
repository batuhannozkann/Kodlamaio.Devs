using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyCommand:IRequest<DeletedTechnologyDto>
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public class DeleteTechnologyCommandHandler:IRequestHandler<DeleteTechnologyCommand,DeletedTechnologyDto>
        {
            private ITechnologyRepository _technologyRepository;
            private IMapper _mapper;
            private TechnologyBusinessRules _technologyBusinessRules;

            public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                Technology? technology = await _technologyRepository.GetAsync(b => b.Id == request.Id && b.LanguageId==request.LanguageId);
                await _technologyBusinessRules.TechnologyShouldExistWhenRequest(request.Id, request.LanguageId);
                Technology deletedTechnology = await _technologyRepository.DeleteAsync(technology);
                DeletedTechnologyDto deletedTechnologyDto = _mapper.Map<DeletedTechnologyDto>(deletedTechnology);
                return deletedTechnologyDto;
            }
        }
    }

}
