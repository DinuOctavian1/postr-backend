using MediatR;
using ErrorOr;

using Application.Authentication.Common;

namespace Application.Authentication.Queries.Login
{
    public record LoginQuery
    (
        string Email,
        string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;
    
}
