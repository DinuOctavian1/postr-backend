namespace Postr.Services.Implementation
{
    public interface IExternalLoginService
    {
        Task<string> GetAccessTokenAsync(string code, string redirectUri);
    }
}
