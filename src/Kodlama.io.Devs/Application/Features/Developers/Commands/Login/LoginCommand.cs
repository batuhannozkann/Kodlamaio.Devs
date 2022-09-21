using Application.Features.Developers.Rules;
using Application.Services;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.Login
{
    public class LoginCommand:UserForLoginDto,IRequest<AccessToken>
    {
        public class LoginCommandHandler:IRequestHandler<LoginCommand,AccessToken>
        {
            private readonly IDeveloperRepository _developerRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly DeveloperBusinessRules _developerBusinessRules;

            public LoginCommandHandler(IDeveloperRepository developerRepository, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, DeveloperBusinessRules developerBusinessRules)
            {
                _developerRepository = developerRepository;
                _tokenHelper = tokenHelper;
                _userOperationClaimRepository = userOperationClaimRepository;
                _developerBusinessRules = developerBusinessRules;
            }

            public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                await _developerBusinessRules.LoginEmailCheck(request.Email);
                Developer developer = await _developerRepository.GetAsync(c => c.Email == request.Email);
                if (!HashingHelper.VerifyPasswordHash(request.Password, developer.PasswordHash, developer.PasswordSalt))
                    throw new BusinessException("Password is incorrect");
                var userOperationClaims = await _userOperationClaimRepository.GetListAsync(c => c.UserId == developer.Id
                , include: c => c.Include(b => b.OperationClaim),cancellationToken:cancellationToken);
                AccessToken token = _tokenHelper.CreateToken(developer, userOperationClaims.Items.Select(c => c.OperationClaim).ToList());
                return token;
            }
        }
    }
}
