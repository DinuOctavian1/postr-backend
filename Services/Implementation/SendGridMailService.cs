using Postr.Configurations;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Postr.Services.Implementation
{
    public class SendGridMailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly EmailConfig _emailConfig;
        

        public SendGridMailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _emailConfig = _configuration.GetSection("EmailConfig").Get<EmailConfig>();
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
           
            var apiKey = _configuration["SendGridAPIKey"];
           
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_emailConfig.EmailAddress, "User");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendEmailConfirmationEmailAsync(string toEmail, string url)
        {
            string subject = _emailConfig.ConfirmationEmailSubj;
            string message = string.Format(_emailConfig.ConfirmationEmailMess, url);

            await SendEmailAsync(toEmail, subject, message);
        }
    }
   
}
