using FluentValidation;

namespace DDDTableTopFriend.Application.Users.Commands.Delete;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
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
