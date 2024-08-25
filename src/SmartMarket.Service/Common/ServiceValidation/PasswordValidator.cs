using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartMarket.Service.Common.ServiceValidation
{
    public static class PasswordValidator
    {
        public static string Symbols { get; } = "~`!@#$%^&*()_-+={[}]|\\:;\"'<,>.?/";

        public static (bool IsValid, string Message) ValidatePassword(string password)
        {
            if (password.Length < 8)
                return (IsValid: false, Message: "Password can not be less than 8 characters");

            Regex pattern = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

            if (!pattern.IsMatch(password))
            {
                if (!password.Any(char.IsUpper))
                    return (IsValid: false, Message: "Password should contain at least one Upper case!");

                if (!password.Any(char.IsLower))
                    return (IsValid: false, Message: "Password should contain at least one Lower case!");

                if (!password.Any(char.IsDigit))
                    return (IsValid: false, Message: "Password should contain at least one Digit!");

                if (!password.Any(ch => Symbols.Contains(ch)))
                    return (IsValid: false, Message: "Password should contain at least one Symbol like (~`!@#$%^&*()_-+={[}]|\\:;\"'<,>.?/");
            }

            return (IsValid: true, Message: "Password is valid.");
        }
    }
}
