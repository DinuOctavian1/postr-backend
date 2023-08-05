namespace Contracts.Authentication
{
    public record AuthenticationResponse(string UserName,
                                         string Email,
                                         string UserId,
                                         string Message);
}
