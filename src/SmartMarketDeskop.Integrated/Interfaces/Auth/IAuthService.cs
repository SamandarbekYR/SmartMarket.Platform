using SmartMarketDesktop.DTOs.DTOs.Auth;
using SmartMarketDesktop.DTOs.DTOs.Worker;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<(bool Result, string Token)> LoginAsync(WorkerLoginDto dto);
    }
}