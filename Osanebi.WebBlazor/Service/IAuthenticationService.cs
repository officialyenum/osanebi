using Microsoft.AspNetCore.Authentication;
using Osanebi.Model.ApplicationModels;
using Osanebi.Model.InputModels;

namespace Osanebi.WebBlazor.Service
{
    public interface IAuthenticationService
    {
        Task<ResponseModel<bool>> LoginAsync(ApplicationUserLoginInputModel model);
        Task<ResponseModel<bool>> RegisterAsync(ApplicationUserRegisterInputModel model);
        Task<ResponseModel<bool>> ConfirmEmailAsync(ApplicationUserConfirmEmailInputModel model);
        Task<ResponseModel<bool>> VerifyEmailCodeAsync(ApplicationUserConfirmEmailInputModel model);
        Task<ResponseModel<bool>> ChangePasswordAsync(ApplicationUserForgotPasswordInputModel model);
        Task<ResponseModel<bool>> ForgotPasswordAsync(ApplicationUserForgotPasswordInputModel model);
    }
}
