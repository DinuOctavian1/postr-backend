using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ErrorOr<AppUser>>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetCurrentUserQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<AppUser>> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(query.Email);
            if (user is null)
                return Errors.Authentication.UserNotFound;

            return user;
        }
    }
}
