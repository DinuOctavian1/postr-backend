using Application.Common.Interfaces.Authentication;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Infrastructure.Authentication
{
    public class SecurityTokenService : ISecurityTokenService
    {
        private readonly UserManager<AppUser> _userManager;

        public SecurityTokenService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Generate2FATokenAsync(AppUser user, string tokenProvider)
        {
            return await _userManager.GenerateTwoFactorTokenAsync(user, tokenProvider);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        public async Task<bool> Validate2FATokenAsync(AppUser user, string tokenProvider, string token)
        {
            return await _userManager.VerifyTwoFactorTokenAsync(user, tokenProvider, token);
        }

        public async Task<bool> ValidateEmailTokenAsync(AppUser user, string token)
        {
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            return result.Succeeded;
        }
    }
}
