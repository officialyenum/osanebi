using Microsoft.Extensions.Localization;
using Osanebi.Model.ApplicationModels;
using Osanebi.Model.InputModels;
using Osanebi.WebBlazor.LocalizationResource;
using System.Net;

namespace Osanebi.WebBlazor.Service
{
    public class AuthenticationService(HttpClient http, IStringLocalizer<Resource> localizer) : IAuthenticationService
    {
        public async Task<ResponseModel<bool>> LoginAsync(ApplicationUserLoginInputModel model)
        {

            var response = await http.PostAsJsonAsync("Identity/Authentication/login", model);

            if (response.IsSuccessStatusCode)
                return new ResponseModel<bool> { IsSuccess = true };

            return await HandleErrorResponse(response);
        }

        public async Task<ResponseModel<bool>> RegisterAsync(ApplicationUserRegisterInputModel model)
        {

            var response = await http.PostAsJsonAsync("Identity/Authentication/register", model);

            if (response.IsSuccessStatusCode)
                return new ResponseModel<bool> { IsSuccess = true };

            return await HandleErrorResponse(response);
        }

        public async Task<ResponseModel<bool>> ConfirmEmailAsync(ApplicationUserConfirmEmailInputModel model)
        {

            var response = await http.PostAsJsonAsync("Identity/Authentication/confirm-email", model);

            if (response.IsSuccessStatusCode)
                return new ResponseModel<bool> { IsSuccess = true };

            return await HandleErrorResponse(response);
        }

        public async Task<ResponseModel<bool>> VerifyEmailCodeAsync(ApplicationUserConfirmEmailInputModel model)
        {
            var response = await http.PostAsJsonAsync("Identity/Authentication/confirm-email-verify-code", model);

            if (response.IsSuccessStatusCode)
                return new ResponseModel<bool> { IsSuccess = true };

            return await HandleErrorResponse(response);
        }

        public async Task<ResponseModel<bool>> ChangePasswordAsync(ApplicationUserForgotPasswordInputModel model)
        {
            var response = await http.PostAsJsonAsync("Identity/Authentication/change-password", model);

            if (response.IsSuccessStatusCode)
                return new ResponseModel<bool>{ IsSuccess = true };

            return await HandleErrorResponse(response);
        }

        public async Task<ResponseModel<bool>> ForgotPasswordAsync(ApplicationUserForgotPasswordInputModel model)
        {
            var response = await http.PostAsJsonAsync("Identity/Authentication/forgot-password", model);

            if (response.IsSuccessStatusCode)
                return new ResponseModel<bool> { IsSuccess = true };

            return await HandleErrorResponse(response);
        }


        private async Task<ResponseModel<bool>> HandleErrorResponse(HttpResponseMessage responseMessage)
        {
            if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResponse = await responseMessage.Content.ReadFromJsonAsync<ResponseModel<bool>>();
                string errorMessage = errorResponse?.Message ?? localizer["message-unknown-error"];
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = GetLocalizedErrorMessage(errorMessage),
                };
            }

            return new ResponseModel<bool>
            {
                IsSuccess = false,
                Message = localizer["message-unknown-error"],
            };
        }

        private string GetLocalizedErrorMessage(string errorMessage)
        {
            var resourceKey = errorMessage switch
            {
                "User is locked out" => "message-user-locked-out",
                "Login is not allowed" => "message-login-not-allowed",
                "Two factor authentication is required" => "message-two-factor-required",
                "Invalid login attempt" => "message-invalid-login",
                "Duplicate Username" => "message-email-exists",
                "User not found" => "message-user-not-found",
                "Email already Confirmed" => "message-email-already-confirmed",
                "Failed to Save Verification Code" => "message-email-verification-code-save-failed",
                "Email Confirmation failed" => "message-email-confirmation-failed",
                "Invalid Confirmation Code" => "message-invalid-confirmation-code",
                "Password change failed" => "message-password-change-failed",
                _ => "message-unknown-error"
            };
            return localizer[resourceKey];
        }

        
    }

}
