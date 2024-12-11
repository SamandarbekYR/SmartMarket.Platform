using System.Text.RegularExpressions;

namespace SmartMarket.Service.Common.ServiceValidation
{
    public static class PhoneNumberValidators
    {
        public static bool IsValid(string phoneNumber)
        {
            string pattern = @"^\+998(33|77|88|50|90|91|99|93|94|95|97)\d{7}$";

            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
