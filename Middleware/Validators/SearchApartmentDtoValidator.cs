using Contracts;
using FluentValidation;

namespace Middleware.Validators
{
    public class SearchApartmentDtoValidator : AbstractValidator<SearchApartmentDto>
    {
        public SearchApartmentDtoValidator()
        {
            RuleFor(o => o.City)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z ]*$");
            RuleFor(o => o.From)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.To)
                .NotNull()
                .NotEmpty();
        }
    }
}
