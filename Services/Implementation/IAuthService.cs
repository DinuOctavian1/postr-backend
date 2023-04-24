using Postr.DTO;
using Postr.Models;
using Postr.ResponseModels;

namespace Postr.Services.Implementation
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterUserAsync(SignupDTO model);
    }
}
