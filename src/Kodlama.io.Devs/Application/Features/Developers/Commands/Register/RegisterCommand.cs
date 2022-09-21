using Application.Features.Developers.Rules;
using Application.Services;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
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

namespace Application.Features.Developers.Commands.Register
{
    public class RegisterCommand:UserForRegisterDto,IRequest<AccessToken>
    {

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccessToken>
        {
            private IDeveloperRepository _developerRepository;
            private IUserOperationClaimRepository _userOperationClaimRepository;
            private IOperationClaimRepository _operationClaimRepository;
            private ITokenHelper _tokenHelper;
            private IMapper _mapper;
            private DeveloperBusinessRules _developerBusinessRules;

            public RegisterCommandHandler(IDeveloperRepository developerRepository, IUserOperationClaimRepository userOperationClaimRepository, IOperationClaimRepository operationClaimRepository, ITokenHelper tokenHelper,IMapper mapper, DeveloperBusinessRules developerBusinessRules)
            {
                _developerRepository = developerRepository;
                _userOperationClaimRepository = userOperationClaimRepository;
                _operationClaimRepository = operationClaimRepository;
                _tokenHelper = tokenHelper;
                _mapper = mapper;
                _developerBusinessRules = developerBusinessRules; 
            }

            public async Task<AccessToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await _developerBusinessRules.RegisterEmailCheck(request.Email);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                var mappedDeveloper = _mapper.Map<Developer>(request);
                mappedDeveloper.PasswordSalt = passwordSalt;
                mappedDeveloper.PasswordHash = passwordHash;
                mappedDeveloper.Status = true;
                mappedDeveloper.AuthenticatorType = AuthenticatorType.Email;
                var registeredDeveloper = await _developerRepository.AddAsync(mappedDeveloper);
                await _userOperationClaimRepository.AddAsync(new UserOperationClaim
                {
                    UserId = registeredDeveloper.Id,
                    OperationClaimId = 2
                });
                var userClaims = await _userOperationClaimRepository.GetListAsync(c => c.UserId == registeredDeveloper.Id
                , include: c => c.Include(x => x.OperationClaim),
                cancellationToken: cancellationToken
                );
                AccessToken accesToken = _tokenHelper.CreateToken(registeredDeveloper, userClaims.Items.Select(c => c.OperationClaim).ToList());
                return accesToken;
            }
        }
    }
}
