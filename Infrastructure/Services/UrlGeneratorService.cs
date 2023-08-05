using Application.Common.Interfaces.Utils;
using Domain.Entities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Infrastructure.Utils
{
    public class UrlGeneratorService : IUrlGeneratorService
    {
        private readonly IConfiguration _configuration;

        public UrlGeneratorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateConfirmationUrl(AppUser user, string emailToken)
        {
            var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            string url = $"{_configuration["ClientUrl"]}/confirm-email?userid={user.Id}&token={validEmailToken}";
            return url;
        }

        public string GenerateResetPasswordUrl(string email, string token)
        {
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["ClientUrl"]}/reset-password?email={email}&&token={validToken}";
            return url;
        }
    }
}
