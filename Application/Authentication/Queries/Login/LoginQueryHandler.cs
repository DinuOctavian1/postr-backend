using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Email;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ISecurityTokenService _securityTokenService;
        private readonly IEmailService _emailService;
        public LoginQueryHandler(UserManager<AppUser> userManager,
                                 IJwtTokenGenerator jwtTokenGenerator,
                                 ISecurityTokenService securityTokenService,
                                 IEmailService emailService)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _securityTokenService = securityTokenService;
            _emailService = emailService;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(query.Email);
            if (user is null)
                return Errors.Authentication.InvalidCredentials;

            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isEmailConfirmed)
                return Errors.Authentication.EmailNotConfirmed;

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, query.Password);
            if (!isPasswordValid)
                return Errors.Authentication.InvalidCredentials;

            if (user.TwoFactorEnabled)
                return await Send2FATokenAsync(user);

            string token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult
            {
                User = user,
                Token = token
            };
        }

        private async Task<ErrorOr<AuthenticationResult>> Send2FATokenAsync(AppUser user)
        {
            string token2FA = await _securityTokenService.Generate2FATokenAsync(user, "Email");
            if (String.IsNullOrEmpty(token2FA))
                return Errors.Authentication.TwoFactorAuthGenerationFailure;

            bool emailResult = await _emailService.Send2FATokenAsync(user.Email, token2FA);
            if (!emailResult)
                return Errors.Authentication.TwoFactorAuthEmailFailure;

            return new AuthenticationResult()
            {
                User = user,
                Message = "Two Factor Authentication code was sent to your email",
            };
        }
    }
}
