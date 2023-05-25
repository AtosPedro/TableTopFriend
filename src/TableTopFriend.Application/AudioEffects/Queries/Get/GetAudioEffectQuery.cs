using TableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.AudioEffects.Queries.Get;

public record GetAudioEffectQuery(
    Guid Id
) : IRequest<ErrorOr<AudioEffectResult>>;
