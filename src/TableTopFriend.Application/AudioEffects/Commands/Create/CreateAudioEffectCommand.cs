using TableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.AudioEffects.Commands.Create;

public record CreateAudioEffectCommand(
    Guid UserId,
    string Name,
    string Description,
    string? AudioLink,
    byte[] AudioClip
) : IRequest<ErrorOr<AudioEffectResult>>;
