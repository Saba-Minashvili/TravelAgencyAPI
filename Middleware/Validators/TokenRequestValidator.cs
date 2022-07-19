using Domain.Entities.Authentication;
using FluentValidation;

namespace Middleware.Validators
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator()
        {
            RuleFor(o => o.Login)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
