using Contracts;
using FluentValidation;

namespace Middleware.Validators
{
    public class ApartmentDtoValidator : AbstractValidator<ApartmentDto>
    {
        public ApartmentDtoValidator()
        {
            RuleFor(o => o.City)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z ]*$");
            RuleFor(o => o.Address)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
            RuleFor(o => o.DistanceToCenter)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(10)
                .WithMessage("Distance to center must be from 0-10km range.");
            RuleFor(o => o.BedsNumber)
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(10)
                .WithMessage("Beds number must be from 1-10 range.");
            RuleFor(o => o.ImageBase64)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.From)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.To)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.Description)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$")
                .MaximumLength(250)
                .WithMessage("Description cannot exceed 150 characters.");
            RuleFor(o => o.HostUserId)
                .NotNull()
                .NotEmpty();
        }
    }
}
