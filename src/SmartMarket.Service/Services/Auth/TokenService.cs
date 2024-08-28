using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Et = SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.Helpers;
using SmartMarket.Service.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.Services.Auth
{
    public class TokenService : ITokenService
    {
        private IConfigurationSection _config;
        public TokenService(IConfiguration configuration)
        {
            _config = configuration.GetSection("Jwt");
        }

        public string GenerateToken(Et.Worker user)
        {
            var identityClaims = new Claim[]
            {
            new Claim ("Id", user.Id.ToString()),
            new Claim ("FirstName", user.FirstName),
            new Claim ("LastName", user.LastName),
            new Claim ("PhoneNumber", user.PhoneNumber),
            new Claim(ClaimTypes.Role, user.WorkerRole.RoleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
            var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int expiresHours = 24!;

            var token = new JwtSecurityToken(
                issuer: _config["Issuer"],
                audience: _config["Audience"],
                claims: identityClaims,
                expires: TimeHelper.GetDateTime().AddHours(expiresHours),
                signingCredentials: keyCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
