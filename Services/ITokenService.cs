using Postr.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Postr.Services
{
        public interface ITokenService
        {
            Task<string> GenerateJWTokenAsync(User user);
            JwtSecurityToken GetValidatedToken(string jwt);
        }
}
