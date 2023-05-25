using FluentValidation;

namespace TableTopFriend.Application.AudioEffects.Commands.Create;

public class CreateAudioEffectCommandValidator : AbstractValidator<CreateAudioEffectCommand>
{
    public CreateAudioEffectCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull();
    }
}
