using FluentValidation;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required")
                .MaximumLength(50)
                .WithMessage("Username must not exceed 50 characters")
                .Matches("^[a-zA-Z0-9 ]*$")
                .WithMessage("Username must contain only alphanumeric characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is invalid");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(5)
                .WithMessage("Password must be at least 6 characters")
                .MaximumLength(50)
                .WithMessage("Password must not exceed 50 characters");
        }
    }
}
