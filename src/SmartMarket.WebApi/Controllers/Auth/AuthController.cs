using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.ServiceValidation;
using SmartMarket.Service.DTOs.Auth;
using SmartMarket.Service.Interfaces.Auth;
using System.Net;

namespace SmartMarket.WebApi.Controllers.Auth
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        public readonly IAuthService _authService = service;
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] WorkerLoginDto loginDto)
        {
            var phoneNumberCheck = PhoneNumberValidators.IsValid(loginDto.PhoneNumber);

            if (phoneNumberCheck is false)
            {
                throw new StatusCodeException(HttpStatusCode.BadRequest, "Phone number invalid");
            }

            var token = await _authService.LoginAsync(loginDto);

            return Ok(new { token.Result, token.Token });
        }
    }
}
