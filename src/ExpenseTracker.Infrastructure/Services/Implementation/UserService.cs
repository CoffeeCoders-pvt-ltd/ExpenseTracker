using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, IUserRepository userRepository, ICrypter crypter)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _crypter = crypter;
        }

        public AuthenticateResponseDto Authenticate(AuthenticateRequestDto model)
        {
            var user = _userRepository.GetQueryable().SingleOrDefault(x => x.Username == model.Username);

            if (user == null) throw new Exception("User not found");

            if (!_crypter.Verify(model.Password, user.Password))
            {
                throw new Exception("Invalid password");
            }
            
            var token = GenerateJwtToken(user);

            return new AuthenticateResponseDto(user, token)
            {
                RememberMe = model.RememberMe,
                ReturnUrl = model.ReturnUrl
            };
        }

        public async Task CreateUser(UserDto dto)
        {
            using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var user = new User(dto.FirstName, dto.LastName, dto.UserName, _crypter.Hash(dto.Password));
            await _userRepository.InsertAsync(user);
            tsc.Complete();
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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