using FluentValidation;

namespace DDDTableTopFriend.Application.Characters.Commands.Create;

public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
{
    public CreateCharacterCommandValidator()
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
