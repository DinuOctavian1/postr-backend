using ErrorOr;
using MediatR;

using Application.Authentication.Common;

namespace Application.Authentication.Commands.Register
{
    public record RegisterCommand
    (
        string Username,
        string Email,
        string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}
