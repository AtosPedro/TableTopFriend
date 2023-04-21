using DDDTableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Update;

public record UpdateAudioEffectCommand(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    string? AudioLink,
    byte[] AudioClip
) : IRequest<ErrorOr<AudioEffectResult>>;
