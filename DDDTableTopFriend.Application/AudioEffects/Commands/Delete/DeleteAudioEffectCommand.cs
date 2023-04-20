using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Delete;

public record DeleteAudioEffectCommand(

) : IRequest<ErrorOr<bool>>;
