using Postr.DTO;
using Postr.Models;
using Postr.ResponseModels;

namespace Postr.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterUserAsync(SignupDTO model);
        Task<AuthResponse> CofirmEmailAsync(EmailConfrimationRequestModel model);
    }
}
