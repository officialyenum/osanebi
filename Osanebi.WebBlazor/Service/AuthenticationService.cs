using Microsoft.Extensions.Localization;
using Osanebi.Model.ApplicationModels;
using Osanebi.Model.InputModels;
using Osanebi.WebBlazor.LocalizationResource;
using System.Net;
using System.Resources;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Osanebi.WebBlazor.Service
{
    public class AuthenticationService(HttpClient http, IStringLocalizer<Resource> localizer) : IAuthenticationService
    {

        public async Task<ResponseModel<bool>> ConfirmEmailAsync(ApplicationUserConfirmEmailInputModel model)
        {
            var response = await http.PostAsJsonAsync("Identity/Authentication/confirm-email", model);
            if (response.IsSuccessStatusCode)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = true,
                };
            }

            return await HandleErrorResponse(response);
        }

        public async Task<ResponseModel<bool>> VerifyEmailCodeAsync(ApplicationUserConfirmEmailInputModel model)
        {
            var response = await http.PostAsJsonAsync("Identity/Authentication/confirm-email-verify-code", model);
            if (response.IsSuccessStatusCode)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = true,
                };
            }

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
                "User not found" => "message-user-not-found",
                "Email already Confirmed" => "message-email-already-confirmed",
                "Failed to Save Verification Code" => "message-email-verification-code-save-failed",
                "Email Confirmation failed" => "message-email-confirmation-failed",
                "Invalid Confirmation Code" => "message-invalid-confirmation-code",
                "Verification code sent Successfully" => "message-email-verification-code-sent-successfully",
                _ => "message-unknown-error"
            };
            return localizer[resourceKey];
        }

    }

}
