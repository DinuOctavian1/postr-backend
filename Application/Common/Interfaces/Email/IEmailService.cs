namespace Application.Common.Interfaces.Email
{
    public interface IEmailService
    {   
        Task<bool> SendConfirmEmailMessageAsync(string email, string url, string username);

        Task<bool> SendResetPasswordEmailAsync(string toEmail, string url);

        Task<bool> Send2FATokenAsync(string toEmail, string code);
    }
}
