namespace Contracts.Authentication
{
    public record Login2FARequest(
        string Email,
        string Token);
}
