using System.IdentityModel.Tokens.Jwt;

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
               // const string V = @"http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
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
                    //case v:
                    //    identity.RoleName = claim.Value;
                    default:
                        break;
                }
            }
            return identity;
        }
    }
}
