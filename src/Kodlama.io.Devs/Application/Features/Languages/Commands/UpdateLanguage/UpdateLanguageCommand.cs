using Application.Features.Languages.Dtos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand:IRequest<UpdatedLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, UpdatedLanguageDto>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public UpdateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedLanguageDto> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            Language mappedLanguage = _mapper.Map<Language>(request);
            
            Language updatedLanguage = await _languageRepository.UpdateAsync(mappedLanguage);
            UpdatedLanguageDto updatedLanguageDto = _mapper.Map<UpdatedLanguageDto>(updatedLanguage);
            return updatedLanguageDto;
        }
    }
}
