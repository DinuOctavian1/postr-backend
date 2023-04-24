using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Postr.Configurations;
using Postr.DTO;
using Postr.Models;
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

        public AuthService(UserManager<User> userManager, IMapper mapper, IConfiguration config, IMailService mailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
            _mailService = mailService;
        }

        public async Task<AuthResponse> RegisterUserAsync(SignupDTO model)
        {
            if (model.Password != model.ConfirmedPassword)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = "Confirmed password does not match the password",
                    Error =  "Confirmed password does not match the password" 
                };
            }

            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Error = result.Errors.Select(e => e.Description).First(),
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
    }
}
