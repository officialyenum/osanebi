using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Osanebi.WebBlazor.Validator
{
    public partial class PasswordAttribute : ValidationAttribute
    {
        private static readonly Regex _regex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", RegexOptions.Compiled);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
                return ValidationResult.Success;

            return _regex.IsMatch(value.ToString()!)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage ??
                    "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one number, and one special character.");
        }
    }

}
