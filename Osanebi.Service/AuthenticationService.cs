using Microsoft.AspNetCore.Identity;
using MimeKit;
using Osanebi.Model.ApplicationModels;
using Osanebi.Model.IdentityModels;
using Osanebi.Model.InputModels;
using Osanebi.Service.IService;
using Osanebi.Utility.Utility;

namespace Osanebi.Service
{
    public class AuthenticationService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IApplicationEmailSender applicationEmailSender
        ) : IAuthenticationService
    {
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

        public async Task<ResponseModel<bool>> ConfirmEmailAsync(ApplicationUserVerificationBaseInputModel model)
        {

            ArgumentNullException.ThrowIfNull(model.Email);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "User not found",
                    Data = false
                };
            }

            if (user.EmailConfirmed)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "Email already Confirmed",
                    Data = false
                };
            }
            user.VerificationCode = GenerateVerificationCode();
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "Failed to Save Verification Code"
                };
            }

            //send email with verification code
            model.Code = user.VerificationCode.ToString();
            model.FullName = $"{user.FirstName} {user.LastName}";
            await SendVerificationEmailAsync(model);
            return new ResponseModel<bool>
            {
                IsSuccess = true,
                Message = "Verification code sent Successfully",
            };
        }

        public async Task<ResponseModel<bool>> ConfirmEmailVerifyCodeAsync(ApplicationUserVerificationBaseInputModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Email);
            ArgumentNullException.ThrowIfNull(model.Code);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }
            bool isCodeValid = user.VerificationCode != null && user.VerificationCode.ToString() == model.Code;
            if (isCodeValid)
            {
                user.EmailConfirmed = true;
                user.Activity = true;
                var result = await userManager.UpdateAsync(user);
                return new ResponseModel<bool>
                {
                    IsSuccess = result.Succeeded,
                    Message = result.Succeeded ? "Email confirmed successfully" : "Email Confirmation failed",
                    Data = true
                };
            }
            else
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "Invalid Confirmation Code",
                    Data = true
                };
            }
        }

        public async Task<ResponseModel<bool>> ForgotPasswordAsync(ApplicationUserForgotPasswordInputModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Email);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "User not found",
                    Data = false
                };
            }
            user.VerificationCode = GenerateVerificationCode();
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "Failed to Save Verification Code"
                };
            }

            //send email with verification code
            model.Code = user.VerificationCode.ToString();
            model.FullName = $"{user.FirstName} {user.LastName}";
            await SendVerificationEmailAsync(model);
            return new ResponseModel<bool>
            {
                IsSuccess = true,
                Message = "Verification code sent Successfully",
            };
        }

        public async Task<ResponseModel<bool>> ChangePasswordAsync(ApplicationUserForgotPasswordInputModel model)
        {
            ArgumentNullException.ThrowIfNull(model.Email);
            ArgumentNullException.ThrowIfNull(model.Code);
            ArgumentNullException.ThrowIfNull(model.Password);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            bool isCodeValid = user.VerificationCode != null && user.VerificationCode.ToString() == model.Code;
            if (isCodeValid)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, model.Password);
                return new ResponseModel<bool>
                {
                    IsSuccess = result.Succeeded,
                    Message = result.Succeeded ? "Password changed successfully" : "Password change failed",
                    Data = true
                };
            }
            else
            {
                return new ResponseModel<bool>
                {
                    IsSuccess = false,
                    Message = "Invalid Confirmation Code",
                    Data = true
                };
            }
        }


        public Task<bool> ResetPasswordAsync(ApplicationUserRegisterInputModel model)
        {
            throw new NotImplementedException();
        }

        private short GenerateVerificationCode()
        {
            Random random = new Random();
            return (short)random.Next(1000, 9999);
        }

        private async Task SendVerificationEmailAsync(ApplicationUserVerificationBaseInputModel model)
        {
            MimeMessage mail = new();
            mail.To.Add(new MailboxAddress(model.FullName, model.Email));
            mail.Subject = "Osanebi - Email Confirmation";
            var emailContent = model.EmailTemplate.Replace("{FullName}", model.FullName)
                                         .Replace("{VerificationCode}", model.Code ?? string.Empty);
            mail.Body = new TextPart("html")
            {
                Text = emailContent
            };

            try
            {
                await applicationEmailSender.SendEmailAsync(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while sending email: " + ex);
            }

        }

    }
}
