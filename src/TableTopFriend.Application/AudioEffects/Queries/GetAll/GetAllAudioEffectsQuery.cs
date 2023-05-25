using TableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.AudioEffects.Queries.GetAll;

public record GetAllAudioEffectsQuery(
    Guid UserId
) : IRequest<ErrorOr<IReadOnlyList<AudioEffectResult>>>;
