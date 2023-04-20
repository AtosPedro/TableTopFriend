using DDDTableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Create;

public record CreateAudioEffectCommand(

) : IRequest<ErrorOr<AudioEffectResult>>;
