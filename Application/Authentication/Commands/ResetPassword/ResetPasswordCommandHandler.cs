using Application.Authentication.Common;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(command.Email);
            if (user is null)
                return Errors.Authentication.UserNotFound;

            var result = await _userManager.ResetPasswordAsync(user, command.Token, command.NewPassword);
            if (!result.Succeeded)
                return Error.Failure(result.Errors.FirstOrDefault().Code, result.Errors.FirstOrDefault().Description);

            return new AuthenticationResult
            {
                Message = "Password reset successfully"
            };
        }
    }
}
