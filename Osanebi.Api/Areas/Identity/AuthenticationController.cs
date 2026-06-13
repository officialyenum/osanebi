using Microsoft.AspNetCore.Mvc;
using Osanebi.Model.InputModels;
using Osanebi.Service.IService;
using System.ComponentModel;

namespace Osanebi.Api.Areas.Identity
{
    [Area("Identity")]
    [DisplayName("Authentication Controller")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("login")]
        [DisplayName("Login Endpoint")]
        public async Task<IActionResult> Login([FromBody] ApplicationUserInputModel model)
        {
            var result = await authenticationService.LoginAsync(model);
            return Ok(result);
        }

        [HttpPost("register")]
        [DisplayName("Register Endpoint")]
        public async Task<IActionResult> Register([FromBody] ApplicationUserInputModel model)
        {
            var result = await authenticationService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        [DisplayName("Forgot Password Endpoint")]
        public async Task<IActionResult> ForgotPassword([FromBody] ApplicationUserInputModel model)
        {
            var result = await authenticationService.ForgotPasswordAsync(model);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        [DisplayName("Reset Password Endpoint")]
        public async Task<IActionResult> ResetPassword([FromBody] ApplicationUserInputModel model)
        {
            var result = await authenticationService.ResetPasswordAsync(model);
            return Ok(result);
        }

        [HttpPost("change-password")]
        [DisplayName("Change Password Endpoint")]
        public async Task<IActionResult> ChangePassword([FromBody] ApplicationUserInputModel model)
        {
            var result = await authenticationService.ChangePasswordAsync(model);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [DisplayName("Refresh Token Endpoint")]
        public async Task<IActionResult> RefreshToken([FromBody] ApplicationUserInputModel model)
        {
            var result = await authenticationService.RefreshTokenAsync(model);
            return Ok(result);
        }
    }
}
