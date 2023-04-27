using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Postr.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Postr.Services.Implementation
{
    public class JwtService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public JwtService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<string> GenerateJWTokenAsync(User user)
        {
            var jwtSettings = _config.GetSection("Jwt");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expireTime = DateTime.Now.AddDays(Double.Parse(jwtSettings.GetSection("ExpireTimeInDays").Value));


            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                audience: jwtSettings.GetSection("Audience").Value,
                expires: expireTime,
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
