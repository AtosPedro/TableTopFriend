using FluentValidation;

namespace TableTopFriend.Application.AudioEffects.Queries.GetAll;

public class GetAllAudioEffectsQueryValidator : AbstractValidator<GetAllAudioEffectsQuery>
{
    public GetAllAudioEffectsQueryValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .NotNull();
    }
}
