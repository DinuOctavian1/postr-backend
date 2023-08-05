using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Email;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Queries.ForgotPassword
{
    public class ForgotPasswordQueryHandler : IRequestHandler<ForgotPasswordQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IEmailService _emailService;
        private readonly ISecurityTokenService _securityTokenService;
        private readonly UserManager<AppUser> _userManager;

        public ForgotPasswordQueryHandler(UserManager<AppUser> userManager, ISecurityTokenService securityTokenService, IEmailService emailService)
        {
            _userManager = userManager;
            _securityTokenService = securityTokenService;
            _emailService = emailService;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(ForgotPasswordQuery query, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(query.Email);
            if (user is null)
                return Errors.Authentication.UserNotFound;

            string token = await _securityTokenService.GeneratePasswordResetTokenAsync(user);

            if (string.IsNullOrEmpty(token))
                return Errors.Authentication.InvalidEmailToken;

            bool emailResult = await _emailService.SendResetPasswordEmailAsync(user.Email, token);
            if (!emailResult)
                return Errors.Authentication.EmailNotSent;

            return new AuthenticationResult
            {
                Message = "Password reset link has been sent to your email address."
            };
        }

    }
}
