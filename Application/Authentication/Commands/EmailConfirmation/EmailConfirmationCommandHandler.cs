using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.EmailConfirmation
{
    public class EmailConfirmationCommandHandler : IRequestHandler<EmailConfirmationCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISecurityTokenService _emailTokenService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public EmailConfirmationCommandHandler(UserManager<AppUser> userManager,
                                               ISecurityTokenService emailTokenService,
                                               IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _emailTokenService = emailTokenService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(EmailConfirmationCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);

            if (user is null)
                return Errors.Authentication.UserNotFound;

            var result = await _emailTokenService.ValidateEmailTokenAsync(user, command.Token);

            if (!result)
                return Errors.Authentication.InvalidEmailToken;

            string token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult
            {
                User = user,
                Token = token
            };
        }
    }
}
