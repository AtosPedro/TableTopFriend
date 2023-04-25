using FluentValidation;

namespace DDDTableTopFriend.Application.Users.Commands.Validate;

public class ValidateUserCommandValidator : AbstractValidator<ValidateUserCommand>
{
    public ValidateUserCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull()
            .NotEmpty();
    }
}
