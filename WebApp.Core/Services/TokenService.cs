using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApp.Core.ServiceContracts;
using WebApp.Core.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace WebApp.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(Member member)
        {
            var tokenKey = _configuration["TokenKey"] ?? throw new Exception("cannot access token key from appsettings");
            if (tokenKey.Length < 64)
                throw new Exception("your tokenkey is short");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, member.MemberLoginName),
            };
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}