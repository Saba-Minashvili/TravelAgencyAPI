using Contracts;
using FluentValidation;

namespace Middleware.Validators
{
    public class BookRequestDtoValidator : AbstractValidator<BookRequestDto>
    {
        public BookRequestDtoValidator()
        {
            RuleFor(o => o.GuestUserId)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.HostUserId)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.From)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.To)
                .NotNull()
                .NotEmpty();
        }
    }
}
