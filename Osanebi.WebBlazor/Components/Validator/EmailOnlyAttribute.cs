using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Osanebi.WebBlazor.Components.Validator
{
    public partial class EmailOnlyAttribute : ValidationAttribute
    {
        private static readonly Regex _regex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
                return ValidationResult.Success;

            return _regex.IsMatch(value.ToString()!)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage ??
                    "Invalid Email");
        }
    }
}
