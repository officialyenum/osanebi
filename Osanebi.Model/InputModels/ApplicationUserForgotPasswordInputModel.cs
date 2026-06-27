using System.ComponentModel.DataAnnotations;

namespace Osanebi.Model.InputModels
{
    public class ApplicationUserForgotPasswordInputModel : ApplicationUserVerificationBaseInputModel
    {
        [Required]
        public string? Password { get; set; }
    }


}
