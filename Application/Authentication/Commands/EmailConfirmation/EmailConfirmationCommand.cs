using ErrorOr;
using MediatR;

using Application.Authentication.Common;

namespace Application.Authentication.Commands.EmailConfirmation
{
    public record EmailConfirmationCommand
    (
        string UserId,
        string Token
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}
