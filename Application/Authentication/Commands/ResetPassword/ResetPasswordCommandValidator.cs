using FluentValidation;

namespace Application.Authentication.Commands.ResetPassword
{
    internal class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is invalid");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("New password is required")
                .MinimumLength(5)
                .WithMessage("New password must be at least 5 characters")
                .MaximumLength(50)
                .WithMessage("New password must not exceed 50 characters");
        }
    }
}
