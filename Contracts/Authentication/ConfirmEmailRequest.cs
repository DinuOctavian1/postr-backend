namespace Contracts.Authentication
{
    public record ConfirmEmailRequest(
        string UserId,
        string Token
    );
}
