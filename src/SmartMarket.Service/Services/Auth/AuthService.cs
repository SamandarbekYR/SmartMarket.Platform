using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Security;
using SmartMarket.Service.DTOs.Auth;
using SmartMarket.Service.Interfaces.Auth;
using System.Net;

namespace SmartMarket.Service.Services.Auth
{
    public class AuthService(IUnitOfWork unitOfWork,
                             ITokenService tokenService) : IAuthService
    {
        public IUnitOfWork _unitOfWork = unitOfWork;
        public ITokenService _tokenService = tokenService;
        public async Task<(bool Result, string Token)> LoginAsync(WorkerLoginDto dto)
        {
            var worker = await _unitOfWork.Worker.GetPhoneNumberAsync(dto.PhoneNumber);

            if (worker is null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Worker not found");
            }

            var hashResult = PasswordHasher.Verify(dto.Password, worker.PasswordSalt, worker.PasswordHash);

            if (hashResult is false)
                throw new StatusCodeException(HttpStatusCode.Unauthorized, "Password is invalid");

            var token = _tokenService.GenerateToken(worker);

            return (Result: true, Token: token);
        }
    }
}
