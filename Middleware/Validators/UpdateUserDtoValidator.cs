using Contracts;
using FluentValidation;

namespace Middleware.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(o => o.FirstName)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z ]*$")
                .MaximumLength(10)
                .WithMessage("FirstName cannot exceed 10 characters.");
            RuleFor(o => o.LastName)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z ]*$")
                .MaximumLength(15)
                .WithMessage("LastName cannot exceed 15 characters.");
            RuleFor(o => o.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            RuleFor(o => o.Description)
                .Matches("^[a-zA-Z0-9 ]*$")
                .MaximumLength(250)
                .WithMessage("Description cannot exceed 250 characters.");
        }
    }
}
