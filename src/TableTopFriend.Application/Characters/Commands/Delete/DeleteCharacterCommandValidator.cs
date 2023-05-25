using FluentValidation;

namespace TableTopFriend.Application.Characters.Commands.Delete;

public class DeleteCharacterCommandValidator : AbstractValidator<DeleteCharacterCommand>
{
    public DeleteCharacterCommandValidator()
    {
        RuleFor(c => c.CharacterId)
            .NotNull()
            .NotEmpty();
    }
}
