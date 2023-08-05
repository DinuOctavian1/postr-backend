using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.Enable2FA
{
    public record Enable2FACommand(string Email) : IRequest<ErrorOr<AuthenticationResult>>;
}
