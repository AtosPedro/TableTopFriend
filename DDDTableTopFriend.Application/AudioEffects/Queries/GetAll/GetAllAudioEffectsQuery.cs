using DDDTableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Queries.GetAll;

public record GetAllAudioEffectsQuery(

) : IRequest<ErrorOr<IReadOnlyList<AudioEffectResult>>>;
