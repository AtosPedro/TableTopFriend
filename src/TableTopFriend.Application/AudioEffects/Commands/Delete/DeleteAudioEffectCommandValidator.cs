using FluentValidation;

namespace TableTopFriend.Application.AudioEffects.Commands.Delete;

public class DeleteAudioEffectCommandValidator : AbstractValidator<DeleteAudioEffectCommand>
{
    public DeleteAudioEffectCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .NotNull();
    }
}
