using FluentValidation;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Delete;

public class DeleteAudioEffectCommandValidator : AbstractValidator<DeleteAudioEffectCommand>
{
    public DeleteAudioEffectCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .NotNull();
    }
}
