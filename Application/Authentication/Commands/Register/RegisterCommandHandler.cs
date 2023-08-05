using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Email;
using Application.Common.Interfaces.Utils;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ISecurityTokenService _emailTokenGenerator;
        private readonly IUrlGeneratorService _urlGenerator;

        public RegisterCommandHandler(UserManager<AppUser> userManager,
                                      IEmailService emailService,
                                      ISecurityTokenService emailTokenGenerator,
                                      IUrlGeneratorService urlGenerator)
        {
            _userManager = userManager;
            _emailService = emailService;
            _emailTokenGenerator = emailTokenGenerator;
            _urlGenerator = urlGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            AppUser user = new()
            {
                Email = command.Email,
                UserName = command.Username
            };

            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
                return Error.Failure(result.Errors.First().Code, result.Errors.First().Description);

            string emailToken = await _emailTokenGenerator.GenerateEmailConfirmationTokenAsync(user);
            if (emailToken is null)
                return Errors.Authentication.EmailTokenNotGenerated;

            string url = _urlGenerator.GenerateConfirmationUrl(user, emailToken);
            if (url is null)
                return Errors.Authentication.UrlNotGenerated;

            bool emailResult = await _emailService.SendConfirmEmailMessageAsync(user.Email, url, user.UserName);
            if (!emailResult)
                return Errors.Authentication.EmailNotSent;

            return new AuthenticationResult
            {
                Message = "An email has been sent to your email address, please confirm your email"
            };
        }
    }
}
