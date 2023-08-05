using Domain.Entities;

namespace Application.Common.Interfaces.Authentication
{
    public interface ISecurityTokenService
    {
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);

        Task<bool> ValidateEmailTokenAsync(AppUser user, string token);

        Task<string> GeneratePasswordResetTokenAsync(AppUser user); 

        Task<string> Generate2FATokenAsync(AppUser user, string tokenProvider);

        Task<bool> Validate2FATokenAsync(AppUser user, string tokenProvider, string token);
    }
}
