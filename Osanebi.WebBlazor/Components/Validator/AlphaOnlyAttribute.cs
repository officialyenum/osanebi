using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Osanebi.WebBlazor.Components.Validator
{
    public class AlphaOnlyAttribute : ValidationAttribute
    {
        private static readonly Regex _regex = new(@"^[a-zA-Z]+$", RegexOptions.Compiled);
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
                return ValidationResult.Success;

            return _regex.IsMatch(value.ToString()!)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage ??
                    "Alpha Only Allowed");

        }
    }
}
