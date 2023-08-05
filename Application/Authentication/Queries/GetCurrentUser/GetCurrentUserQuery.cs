using Domain.Entities;

using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.GetCurrentUser
{
    public record GetCurrentUserQuery(string Email) : IRequest<ErrorOr<AppUser>>;
}
