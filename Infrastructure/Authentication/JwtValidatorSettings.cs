using Microsoft.Extensions.Configuration;
namespace Infrastructure.Authentication
{
    public class JwtValidatorSettings
    {
        public bool ValidateIssuer = true;
        public bool ValidateAudience = true;
        public bool ValidateLifetime = true;
        public bool ValidateIssuerSigningKey = true;
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }

    }
}
