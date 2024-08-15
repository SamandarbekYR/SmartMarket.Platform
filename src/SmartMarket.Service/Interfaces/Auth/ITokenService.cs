using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.Service.Interfaces.Auth
{
    public interface ITokenService
    {
        public string GenerateToken(Worker user);
    }
}
