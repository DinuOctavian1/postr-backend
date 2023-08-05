using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.Login2FA
{
    public record Login2FAQuery(string Email,
                                string Token) : IRequest<ErrorOr<AuthenticationResult>>;
}
