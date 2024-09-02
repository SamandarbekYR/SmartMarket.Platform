using SmartMarketDesktop.DTOs.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Interfaces.Auth
{
    public interface IAuthService
    {
        public Task<(bool Result, string Token)> LoginAsync(UserLoginDto dto);
    }
}
