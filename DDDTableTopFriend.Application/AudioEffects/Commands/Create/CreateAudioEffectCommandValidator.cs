using FluentValidation;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Create;

public class CreateAudioEffectCommandValidator : AbstractValidator<CreateAudioEffectCommand>
{
    public CreateAudioEffectCommandValidator()
    {
    }
}
