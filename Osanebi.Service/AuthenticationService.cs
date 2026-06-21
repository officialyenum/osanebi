using Microsoft.AspNetCore.Identity;
using Osanebi.Model.ApplicationModels;
using Osanebi.Model.IdentityModels;
using Osanebi.Model.InputModels;
using Osanebi.Service.IService;

namespace Osanebi.Service
{
    public class AuthenticationService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager) : IAuthenticationService
    {
        public Task<bool> ChangePasswordAsync(ApplicationUserRegisterInputModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ForgotPasswordAsync(ApplicationUserRegisterInputModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<bool>> LoginAsync(ApplicationUserLoginInputModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = true,
                    Message = "Login successful",
                    Data = true
                };
            }
            string errorMessage = result.IsLockedOut ? "User is locked out." :
                                  result.IsNotAllowed ? "Login is not allowed" :
                                  result.RequiresTwoFactor ? "Two-factor authentication is required." :
                                  "Invalid login attempt.";
            return new ResponseModel<bool>
            {
                IsSuccess = false,
                Message = errorMessage,
                Data = false
            };
        }

        public Task<bool> RefreshTokenAsync(ApplicationUserRegisterInputModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<bool>> RegisterAsync(ApplicationUserRegisterInputModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Email);
            ArgumentNullException.ThrowIfNull(model.Password);

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                RegistrationDate = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = true,
                    Message = "User created successfully",
                    Data = true
                };
            }
            string errorMessage = result.Errors.Any() ? string.Join(", ", result.Errors.Select(x => x.Code)) : "Unable to register user due to unknown errors.";
            return new ResponseModel<bool>
            {
                IsSuccess = false,
                Message = errorMessage,
                Data = false
            };

        }

        public Task<bool> ResetPasswordAsync(ApplicationUserRegisterInputModel model)
        {
            throw new NotImplementedException();
        }
    }
}
