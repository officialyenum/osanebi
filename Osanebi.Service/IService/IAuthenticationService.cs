using Osanebi.Model.ApplicationModels;
using Osanebi.Model.InputModels;

namespace Osanebi.Service.IService
{
    public interface IAuthenticationService
    {
        Task<ResponseModel<bool>> LoginAsync(ApplicationUserLoginInputModel model);
        Task<ResponseModel<bool>> RegisterAsync(ApplicationUserRegisterInputModel nodel);
        Task<ResponseModel<bool>> ConfirmEmailAsync(ApplicationUserVerificationBaseInputModel model);
        Task<ResponseModel<bool>> ConfirmEmailVerifyCodeAsync(ApplicationUserVerificationBaseInputModel model);
        Task<ResponseModel<bool>> ForgotPasswordAsync(ApplicationUserForgotPasswordInputModel model);
        Task<ResponseModel<bool>> ChangePasswordAsync(ApplicationUserForgotPasswordInputModel model);
        Task<bool> ResetPasswordAsync(ApplicationUserRegisterInputModel model);
        Task<bool> RefreshTokenAsync(ApplicationUserRegisterInputModel model);
    }
}
