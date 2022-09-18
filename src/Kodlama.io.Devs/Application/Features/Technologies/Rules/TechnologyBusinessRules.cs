using Application.Services;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly ILanguageRepository _languageRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        

        public async Task TechnologyNameCanNotBeDuplicated(string name,int id)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(b => b.Name.ToLower() == name.ToLower() && b.LanguageId==id);
            if (result.Items.Any()) { throw new BusinessException("Technology name exists in Programming Language"); }
        }
        public async Task TechnologyShouldExistWhenRequest(int technologyId,int languageId)
        {
            Technology result = await _technologyRepository.GetAsync(b=>b.Id==technologyId && b.LanguageId==languageId);
            if (result == null) throw new BusinessException("Requested Technology does not exist");
        }
    }
}
