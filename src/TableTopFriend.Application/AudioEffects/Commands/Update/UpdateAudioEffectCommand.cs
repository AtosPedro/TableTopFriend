using TableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.AudioEffects.Commands.Update;

public record UpdateAudioEffectCommand(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    string? AudioLink,
    byte[] AudioClip
) : IRequest<ErrorOr<AudioEffectResult>>;
