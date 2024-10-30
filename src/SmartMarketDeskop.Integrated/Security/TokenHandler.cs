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
                switch (claim.Type)
                {
                    case "Id":
                        identity.Id = Guid.Parse(claim.Value);
                        break;
                    case "FirstName":
                        identity.FirstName = claim.Value;
                        break;
                    case "LastName":
                        identity.LastName = claim.Value;
                        break;
                    case "PhoneNumber":
                        identity.PhoneNumber = claim.Value;
                        break;
                    case "RoleName":
                        identity.RoleName = claim.Value;
                        break;
                    default:
                        break;
                }
            }
            return identity;
        }
    }
}
