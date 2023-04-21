using FluentValidation;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Update;

public class UpdateAudioEffectCommandValidator : AbstractValidator<UpdateAudioEffectCommand>
{
    public UpdateAudioEffectCommandValidator()
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
