using API.Utilities;
using Application.Authentication.Commands.EmailConfirmation;
using Application.Authentication.Commands.Enable2FA;
using Application.Authentication.Commands.Register;
using Application.Authentication.Commands.ResetPassword;
using Application.Authentication.Common;
using Application.Authentication.Queries.ForgotPassword;
using Application.Authentication.Queries.Login;
using Application.Authentication.Queries.Login2FA;
using Application.Common.Interfaces.Authentication;
using Contracts.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AccountController(IMediator mediator,
                                 IJwtTokenGenerator jwtTokenGenerator,
                                IMapper mapper)
        {
            _mediator = mediator;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            var query = _mapper.Map<LoginQuery>(loginRequest);

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

            return authResult.Match<ActionResult>(
                    authResult =>
                    {
                        if (authResult.Token is not null)
                            SetJtwInCookies(authResult);

                        return Ok(_mapper.Map<AuthenticationResponse>(authResult));
                    },
                    errors => Problem(errors));
        }

        [HttpPost("login-2fa")]
        public async Task<ActionResult> Login2FA(Login2FARequest login2FARequest)
        {
            var querry = _mapper.Map<Login2FAQuery>(login2FARequest);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(querry);

            return authResult.Match<ActionResult>(
                            authResult =>
                                   {
                                       SetJtwInCookies(authResult);
                                       return Ok(_mapper.Map<AuthenticationResponse>(authResult));
                                   },
                            errors => Problem(errors));
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest registerRequest)
        {
            var command = _mapper.Map<RegisterCommand>(registerRequest);

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match<ActionResult>(
                    authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                    errors => Problem(errors));
        }

        [HttpPost("confirm-email")]
        public async Task<ActionResult> ConfirmEmail(ConfirmEmailRequest confirmEmailRequest)
        {

            var command = _mapper.Map<EmailConfirmationCommand>(confirmEmailRequest);

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match<ActionResult>(
                    authResult =>
                    {
                        SetJtwInCookies(authResult);
                        return Ok(_mapper.Map<AuthenticationResponse>(authResult));
                    },
                    errors => Problem(errors)
                    );
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
        {
            var query = _mapper.Map<ForgotPasswordQuery>(forgotPasswordRequest);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

            return authResult.Match<ActionResult>(
                                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                                errors => Problem(errors));
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var command = _mapper.Map<ResetPasswordCommand>(resetPasswordRequest);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match<ActionResult>(
                                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                                errors => Problem(errors));
        }

        [Authorize]
        [HttpGet("enable-2fa")]
        public async Task<ActionResult> Enable2FA()
        {
            string userEmail = User.FindFirstValue(ClaimTypes.Email);
            var command = _mapper.Map<Enable2FACommand>(userEmail);

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match<ActionResult>(
                                              authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                                              errors => Problem(errors));
        }

        [Authorize]
        [HttpGet("user")]
        public Task<ActionResult> GetCurrentUser()
        {
            string test = "test";
            return null;
        }

        private void SetJtwInCookies(AuthenticationResult authResult)
        {
            var cookieOptions = CookieHelper.GetSecureCookieOptions();
            Response.Cookies.Append(_jwtTokenGenerator.GetTokenName, authResult.Token, cookieOptions);
        }

    }
}
