using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Osanebi.WebBlazor.Components.Validator
{
    public class CodeAttribute : ValidationAttribute
    {
        private static readonly Regex _regex = new(@"^\d{4}$", RegexOptions.Compiled);
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
                return ValidationResult.Success;

            return _regex.IsMatch(value.ToString()!)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage ??
                    "Invalid Code");

        }
    }
}
