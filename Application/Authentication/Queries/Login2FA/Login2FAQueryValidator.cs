using FluentValidation;

namespace Application.Authentication.Queries.Login2FA
{
    internal class Login2FAQueryValidator : AbstractValidator<Login2FAQuery>
    {
        public Login2FAQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Token)
                .NotEmpty();
        }
    }
}
