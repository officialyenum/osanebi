using System.ComponentModel.DataAnnotations;

namespace Osanebi.Model.InputModels
{
    public class ApplicationUserLoginInputModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public required string? Password { get; set; }
    }
}
