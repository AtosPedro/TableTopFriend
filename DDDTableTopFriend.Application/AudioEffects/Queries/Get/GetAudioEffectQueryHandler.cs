using DDDTableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Queries.Get;

public class GetAudioEffectQueryHandler : IRequestHandler<GetAudioEffectQuery, ErrorOr<AudioEffectResult>>
{
    public Task<ErrorOr<AudioEffectResult>> Handle(
        GetAudioEffectQuery request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
