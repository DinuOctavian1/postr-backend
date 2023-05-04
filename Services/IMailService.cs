namespace Postr.Services
{
    public interface IMailService
    {
        Task SendEmailConfirmationEmailAsync(string email, string url);
    }
}
