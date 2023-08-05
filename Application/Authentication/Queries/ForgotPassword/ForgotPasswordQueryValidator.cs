using FluentValidation;
namespace Application.Authentication.Queries.ForgotPassword
{
    internal class ForgotPasswordQueryValidator : AbstractValidator<ForgotPasswordQuery>
    {
        public ForgotPasswordQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    };
}
