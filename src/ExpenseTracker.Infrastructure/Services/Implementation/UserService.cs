using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ExpenseTracker.Common.DBAL;
using ExpenseTracker.Core.Crypter;
using ExpenseTracker.Core.Dto;
using ExpenseTracker.Core.Dto.User;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Infrastructure.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICrypter _crypter;
        private readonly IUow _uow;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, IUserRepository userRepository, ICrypter crypter, IUow uow)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _crypter = crypter;
            _uow = uow;
        }

        public AuthenticateResponseDto Authenticate(AuthenticateRequestDto model)
        {
            var user = _userRepository.GetQueryable().SingleOrDefault(x => x.Username == model.Username);

            if (user == null) throw new Exception("User not found");

            if (!_crypter.Verify(model.Password, user.Password))
            {
                throw new Exception("Invalid password");
            }

            var token = GenerateJwtToken(user.Id);

            return new AuthenticateResponseDto(user, token)
            {
                RememberMe = model.RememberMe,
                ReturnUrl = model.ReturnUrl
            };
        }

        public async Task CreateUser(UserDto dto)
        {
            using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var existingUser = await _userRepository.ExistingUserName(dto.UserName);
            if (existingUser) throw new Exception("Duplicate user name.");
            var user = new User(dto.FirstName, dto.LastName, dto.UserName, _crypter.Hash(dto.Password));
            await _userRepository.CreateAsync(user);
            await _uow.CommitAsync();
            tsc.Complete();
        }

        private string GenerateJwtToken(long userId)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,
                    new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };
            SymmetricSecurityKey symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSecret()));
            SigningCredentials signingCredential =
                new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            JwtHeader jwtHeader = new JwtHeader(signingCredential);
            JwtPayload jwtPayload = new JwtPayload(claims);
            JwtSecurityToken token = new JwtSecurityToken(jwtHeader, jwtPayload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}