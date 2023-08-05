using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ResetPassword
{
    public record ResetPasswordCommand(
        string Email,
        string Token,
        string NewPassword
           ) : IRequest<ErrorOr<AuthenticationResult>>;
}
