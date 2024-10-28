using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;

namespace SmartMarketDeskop.Integrated.Security
{
    public static class TokenHandler
    {
        public static IdentitySingelton ParseToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenInfo = tokenHandler.ReadJwtToken(token);

            var identity = new IdentitySingelton
            {
                Token = token
            };

            foreach (var claim in tokenInfo.Claims)
            {
                identity = claim.Type switch
                {
                    "Id" => new IdentitySingelton { Id = Guid.Parse(claim.Value) },
                    "FirstName" => new IdentitySingelton { FirstName = claim.Value },
                    "LastName" => new IdentitySingelton { LastName = claim.Value },
                    "PhoneNumber" => new IdentitySingelton { PhoneNumber = claim.Value },
                    "RoleName" => new IdentitySingelton { RoleName = claim.Value },
                    _ => identity 
                };
            }
            return identity;
        }
    }
}
