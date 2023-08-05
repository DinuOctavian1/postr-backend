namespace Infrastructure.Mail
{
    public class EmailSettings
    {
        public const string SectionName = "EmailServiceConfig";
        public string EmailAddress { get; set; }
        public string SendGridAPIKey { get; set; }
    }
}
