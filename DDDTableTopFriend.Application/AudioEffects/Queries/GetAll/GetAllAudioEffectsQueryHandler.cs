using DDDTableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Queries.GetAll;

public class GetAllAudioEffectsQueryHandler : IRequestHandler<GetAllAudioEffectsQuery, ErrorOr<IReadOnlyList<AudioEffectResult>>>
{
    public Task<ErrorOr<IReadOnlyList<AudioEffectResult>>> Handle(
        GetAllAudioEffectsQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
