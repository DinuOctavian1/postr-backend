using Application.Authentication.Common;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.Enable2FA
{
    public class Enable2FACommandHandler : IRequestHandler<Enable2FACommand, ErrorOr<AuthenticationResult>>
    {
        private readonly UserManager<AppUser> _userManager;

        public Enable2FACommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(Enable2FACommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return Errors.Authentication.UserNotFound;

            var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
            if (!result.Succeeded)
                return Errors.Authentication.TwoFactorAuthNotEnabled;

            return new AuthenticationResult()
            {
                Message = "Two factor authentication enabled successfully"
            };
        }
    }
}
