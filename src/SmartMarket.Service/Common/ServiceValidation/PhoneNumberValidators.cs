using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartMarket.Service.Common.ServiceValidation
{
    public static class PhoneNumberValidators
    {
        public static bool IsValid(string phoneNumber)
        {
            string pattern = @"^\+998\d{9}$";

            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
