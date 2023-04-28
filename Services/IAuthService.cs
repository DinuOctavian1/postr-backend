using Postr.DTO;
using Postr.Models;
using Postr.RequestModels;
using Postr.ResponseModels;

namespace Postr.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterUserAsync(SignupDTO model);
        Task<AuthResponse> CofirmEmailAsync(EmailConfirmationRequestModel model);
        Task<User> GetUserfromTokenAsync(string jwt);
        Task<AuthResponse> LoginAsync(LoginDTO model);
    }
}
