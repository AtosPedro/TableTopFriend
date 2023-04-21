using FluentValidation;

namespace DDDTableTopFriend.Application.AudioEffects.Queries.Get;

public class GetAudioEffectQueryValidator : AbstractValidator<GetAudioEffectQuery>
{
    public GetAudioEffectQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .NotNull();
    }
}
