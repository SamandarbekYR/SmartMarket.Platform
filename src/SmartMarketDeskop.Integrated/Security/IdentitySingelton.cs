using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Security
{
    public class IdentitySingelton
    {
        public static IdentitySingelton _identitySingelton;
        public string Token { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public static IdentitySingelton GetInstance()
        {
            if (_identitySingelton == null)
            {
                _identitySingelton = new IdentitySingelton();
            }

            return _identitySingelton;
        }
    }
}
