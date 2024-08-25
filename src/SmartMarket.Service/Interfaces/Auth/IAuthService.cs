using SmartMarket.Service.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.Interfaces.Auth
{
    public interface IAuthService
    {
        public Task<(bool Result, string Token)> LoginAsync(WorkerLoginDto dto);
    }
}
