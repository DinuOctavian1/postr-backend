using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.ForgotPassword
{
    public record ForgotPasswordQuery(
        string Email
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}
