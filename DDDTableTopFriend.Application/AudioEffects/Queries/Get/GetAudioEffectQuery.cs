using DDDTableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Queries.Get;

public record GetAudioEffectQuery(

) : IRequest<ErrorOr<AudioEffectResult>>;
