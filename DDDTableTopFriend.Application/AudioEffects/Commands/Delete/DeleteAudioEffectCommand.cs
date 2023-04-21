using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Delete;

public record DeleteAudioEffectCommand(
    Guid Id
) : IRequest<ErrorOr<bool>>;
