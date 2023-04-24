using FluentValidation;

namespace DDDTableTopFriend.Application.Characters.Commands.Delete;

public class DeleteCharacterCommandValidator : AbstractValidator<DeleteCharacterCommand>
{
    public DeleteCharacterCommandValidator()
    {
        RuleFor(c => c.CharacterId)
            .NotNull()
            .NotEmpty();
    }
}
