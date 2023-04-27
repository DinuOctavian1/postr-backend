using Postr.Models;

namespace Postr.Services
{
        public interface ITokenService
        {
            Task<string> GenerateJWTokenAsync(User user);
        }
}
