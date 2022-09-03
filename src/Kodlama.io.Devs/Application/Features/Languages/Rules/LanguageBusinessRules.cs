using Application.Services;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Rules
{
    public class LanguageBusinessRules
    {
        private ILanguageRepository _languageRepository;

        public LanguageBusinessRules(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }
        public async Task LanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Language> result = await _languageRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Language name exists.");
        }
        public async Task LanguageShouldExistWhenRequested(Language language)
        {
            if (language == null) throw new BusinessException("Requested Language does not exist");
        }
        public async Task LanguageShouldExistWhenRequested(string name)
        {
            if (!String.IsNullOrEmpty(name)) throw new BusinessException("Requested Language does not exist");
        }
    }
}
