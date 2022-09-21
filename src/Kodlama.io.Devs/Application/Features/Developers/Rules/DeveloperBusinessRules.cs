using Application.Services;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Rules
{
    public class DeveloperBusinessRules
    {
        private readonly IDeveloperRepository _developerRepository;

        public DeveloperBusinessRules(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }
        public async Task LoginEmailCheck(string email)
        {
            var user =await _developerRepository.GetAsync(c => c.Email == email);
            if (user == null) throw new BusinessException("Email does not exist in the system");
        }
        public async Task RegisterEmailCheck(string email)
        {
            var user = await _developerRepository.GetAsync(c => c.Email == email);
            if (user != null) throw new BusinessException("Email exist in the system");
        }
    }
}
