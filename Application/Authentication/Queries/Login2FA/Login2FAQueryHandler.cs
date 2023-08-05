using Domain.Common.Errors;
using Application.Authentication.Common;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Common.Interfaces.Authentication;

namespace Application.Authentication.Queries.Login2FA
{
    public class Login2FAQueryHandler : IRequestHandler<Login2FAQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISecurityTokenService _tokenService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public Login2FAQueryHandler(UserManager<AppUser> userManager,
                                    ISecurityTokenService tokenService,
                                    IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(Login2FAQuery query, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(query.Email);
            if (user is null)
                return Errors.Authentication.UserNotFound;

            if (!user.TwoFactorEnabled)
                return Errors.Authentication.TwoFactorAuthNotEnabled;

            bool result = await _tokenService.Validate2FATokenAsync(user, "Email", query.Token);
            if (!result)
                return Errors.Authentication.TwoFactorAuthInvalidToken;

            string jwtToken = _jwtTokenGenerator.GenerateToken(user);
            if (String.IsNullOrEmpty(jwtToken))
                return Errors.Authentication.JwtTokenGenerationFailed;

            return new AuthenticationResult
            {
                Message = "Login successful",
                User = user,
                Token = jwtToken
            };
        }
    }
}
