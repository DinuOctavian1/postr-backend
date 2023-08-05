using Application.Common.Interfaces.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Mail
{
    public class SendGridMailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly EmailTemplates _emailTemplates;

        public SendGridMailService(IOptions<EmailSettings> emailSettings,
                                   IOptions<EmailTemplates> emailTemplates)
        {
            _emailSettings = emailSettings.Value;
            _emailTemplates = emailTemplates.Value;
        }

        public async Task<bool> Send2FATokenAsync(string toEmail, string code)
        {
            string subject = _emailTemplates.TwoFactorAuthSubject;
            string message = _emailTemplates.Get2FABody(code);
            
            return await SendEmailAsync(toEmail, subject, message);
        }

        public async Task<bool> SendConfirmEmailMessageAsync(string toEmail, string url, string username)
        {
           
            string subject = _emailTemplates.RegistrationConfirmationSubject;
            string message = _emailTemplates.GetRegistrationConfirmationBody(username, url);

            return await SendEmailAsync(toEmail, subject, message);
        }

        public async Task<bool> SendResetPasswordEmailAsync(string toEmail, string url)
        {
            var apiKey = _emailSettings.SendGridAPIKey;
            string subject = _emailTemplates.ForgotPasswordSubject;
            string message = _emailTemplates.GetForgotPasswordBody(url);

            return await SendEmailAsync(toEmail, subject, message);
        }

        private async Task<bool> SendEmailAsync(string toEmail, string subject, string content)
        {

            var apiKey = _emailSettings.SendGridAPIKey;

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_emailSettings.EmailAddress, "User");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var result =  await client.SendEmailAsync(msg);
            return result.IsSuccessStatusCode;
        }

    }
}
