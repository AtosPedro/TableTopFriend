using DDDTableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Update;

public record UpdateAudioEffectCommand(

) : IRequest<ErrorOr<AudioEffectResult>>;
