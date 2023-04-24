using FluentValidation;

namespace DDDTableTopFriend.Application.Characters.Commands.Update;

public class UpdateCharacterCommandValidator : AbstractValidator<UpdateCharacterCommand>
{
    public UpdateCharacterCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Type)
            .NotEmpty()
            .NotNull();
    }
}
