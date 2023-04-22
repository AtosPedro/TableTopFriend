using FluentValidation;

namespace DDDTableTopFriend.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(8);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .NotNull()
            .Length(3, 20);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .NotNull()
            .Length(3, 20);
    }
}
