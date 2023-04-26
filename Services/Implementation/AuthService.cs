using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Postr.Configurations;
using Postr.Constants;
using Postr.DTO;
using Postr.Models;
using Postr.RequestModels;
using Postr.ResponseModels;
using System.Text;

namespace Postr.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IMailService _mailService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<User> userManager, IMapper mapper, IConfiguration config, IMailService mailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
            _mailService = mailService;
        }

        public async Task<AuthResponse> CofirmEmailAsync(EmailConfrimationRequestModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
               return new AuthResponse
               {
                   IsSuccess = false,
                   Message = "User not found"
               };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                bool roleResult = await AssignRoleAsync(user, UserType.USER);
                if (roleResult) 
                { 
                    return new AuthResponse
                    {
                        IsSuccess = true,
                        Message = "Email confirmed successfully",
                        Data = _mapper.Map<UserDTO>(user)
                    };
                }
                return new AuthResponse
                {
                    IsSuccess = true,
                    Message = "Unable to assign a role. Please contact support",
                };
            }

            return new AuthResponse
            {
                IsSuccess = false,
                Message = "Email cannot be confirmed"
            };
        }

        public async Task<AuthResponse> RegisterUserAsync(SignupDTO model)
        {
            if (model.Password != model.ConfirmedPassword)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message =  "Confirmed password does not match the password" 
                };
            }

            var user = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = result.Errors.Select(e => e.Description).First(),
                };
            }

            var confirmationEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmationEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            string url = $"{_config["AppUrl"]}/confirm-email?userid={user.Id}&token={validEmailToken}";

            string emailMessage = EmailServiceConfig.GetEmailConfirmationMessage(url);
            await _mailService.SendEmailAsync(user.Email, EmailServiceConfig.confirmationEmailSubject, emailMessage);

            return new AuthResponse
            {
                IsSuccess = true,
                Message = $"A link has been send to your address {user.Email}. Please confirm your email address!"
            };

        }

        private async Task<bool> AssignRoleAsync(User user, UserType userType)
        {
            var role = UserRoles.User;

            switch (userType)
            {
                case UserType.ADMIN:
                    role = UserRoles.Admin; 
                    break;
            }


            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }
    }
}
