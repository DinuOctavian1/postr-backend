namespace Infrastructure.Authentication
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string Name { get; } = "PostrJwt";
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpireTimeInDays { get; set; }
    }
}
