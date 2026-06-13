using Osanebi.Model.ApplicationModels;
using Osanebi.Model.InputModels;

namespace Osanebi.Service.IService
{
    public interface IAuthenticationService
    {
        Task<ResponseModel<bool>> LoginAsync(ApplicationUserLoginInputModel model);
        Task<bool> RegisterAsync(ApplicationUserRegisterInputModel nodel);
        Task<bool> ForgotPasswordAsync(ApplicationUserRegisterInputModel model);
        Task<bool> ResetPasswordAsync(ApplicationUserRegisterInputModel model);
        Task<bool> ChangePasswordAsync(ApplicationUserRegisterInputModel model);
        Task<bool> RefreshTokenAsync(ApplicationUserRegisterInputModel model);
    }
}
