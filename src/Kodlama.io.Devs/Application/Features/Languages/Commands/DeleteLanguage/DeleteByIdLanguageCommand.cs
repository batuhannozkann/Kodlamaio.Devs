using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.DeleteLanguage
{
    public class DeleteByIdLanguageCommand:IRequest<DeletedLanguageDto>
    {
        public int Id { get; set; }
        public class DeleteByIdLanguageCommandHandler : IRequestHandler<DeleteByIdLanguageCommand, DeletedLanguageDto>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
            private readonly LanguageBusinessRules _languageBusinessRules; 

            public DeleteByIdLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
                _languageBusinessRules = languageBusinessRules;
            }

            public async Task<DeletedLanguageDto> Handle(DeleteByIdLanguageCommand request, CancellationToken cancellationToken)
            {
                Language? language = await _languageRepository.GetAsync(b => b.Id == request.Id);
                await _languageBusinessRules.LanguageShouldExistWhenRequested(language);
                Language deletedLanguage = await _languageRepository.DeleteAsync(language);
                DeletedLanguageDto deletedLanguageDto = _mapper.Map<DeletedLanguageDto>(deletedLanguage);
                return deletedLanguageDto;
            }
        }
    }
}
