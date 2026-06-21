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
        public async Task<IActionResult> Login([FromBody] ApplicationUserLoginInputModel model)
        {
            var result = await authenticationService.LoginAsync(model);
            return result.IsSuccess ? Ok() : BadRequest(result);
        }

        [HttpPost("register")]
        [DisplayName("Register Endpoint")]
        public async Task<IActionResult> Register([FromBody] ApplicationUserRegisterInputModel model)
        {
            var result = await authenticationService.RegisterAsync(model);
            return result.IsSuccess ? Ok() : BadRequest(result);
        }

        [HttpPost("confirm-email")]
        [DisplayName("Confirm Email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ApplicationUserConfirmEmailInputModel model)
        {
            var result = await authenticationService.ConfirmEmailAsync(model);
            return result.IsSuccess ? Ok() : BadRequest(result);
        }

        [HttpPost("confirm-email-verify-code")]
        [DisplayName("Confirm Email Verify Code")]
        public async Task<IActionResult> ConfirmEmailVerifyCode([FromBody] ApplicationUserConfirmEmailInputModel model)
        {
            var result = await authenticationService.ConfirmEmailVerifyCodeAsync(model);
            return result.IsSuccess ? Ok() : BadRequest(result);
        }

        [HttpPost("forgot-password")]
        [DisplayName("Forgot Password Endpoint")]
        public async Task<IActionResult> ForgotPassword([FromBody] ApplicationUserRegisterInputModel model)
        {
            var result = await authenticationService.ForgotPasswordAsync(model);
            return result ? Ok(result) : StatusCode(500);
        }

        [HttpPost("reset-password")]
        [DisplayName("Reset Password Endpoint")]
        public async Task<IActionResult> ResetPassword([FromBody] ApplicationUserRegisterInputModel model)
        {
            var result = await authenticationService.ResetPasswordAsync(model);
            return result ? Ok(result) : StatusCode(500);
        }

        [HttpPost("change-password")]
        [DisplayName("Change Password Endpoint")]
        public async Task<IActionResult> ChangePassword([FromBody] ApplicationUserRegisterInputModel model)
        {
            var result = await authenticationService.ChangePasswordAsync(model);
            return result ? Ok(result) : StatusCode(500);
        }

        [HttpPost("refresh-token")]
        [DisplayName("Refresh Token Endpoint")]
        public async Task<IActionResult> RefreshToken([FromBody] ApplicationUserRegisterInputModel model)
        {
            var result = await authenticationService.RefreshTokenAsync(model);
            return result ? Ok(result) : StatusCode(500);
        }
    }
}
