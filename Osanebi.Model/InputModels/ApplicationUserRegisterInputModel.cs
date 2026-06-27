using Osanebi.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace Osanebi.Model.InputModels
{
    public class ApplicationUserRegisterInputModel
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ImageName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public required string? Password { get; set; }
    }
}
