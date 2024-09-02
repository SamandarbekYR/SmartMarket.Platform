using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.DTOs.DTOs.Auth
{
    public class UserLoginDto
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
