﻿using FluentValidation;

namespace Application.Authentication.Queries.Login
{
    internal class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
