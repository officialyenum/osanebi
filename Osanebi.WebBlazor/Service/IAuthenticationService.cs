using Osanebi.Model.ApplicationModels;
using Osanebi.Model.InputModels;

namespace Osanebi.WebBlazor.Service
{
    public interface IAuthenticationService
    {
        Task<ResponseModel<bool>> ConfirmEmailAsync(ApplicationUserConfirmEmailInputModel model);
        Task<ResponseModel<bool>> VerifyEmailCodeAsync(ApplicationUserConfirmEmailInputModel model);

    }
}
