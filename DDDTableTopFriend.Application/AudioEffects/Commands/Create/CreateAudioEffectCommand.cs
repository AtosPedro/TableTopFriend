using DDDTableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Create;

public record CreateAudioEffectCommand(
    Guid UserId,
    string Name,
    string Description,
    string? AudioLink,
    byte[] AudioClip
) : IRequest<ErrorOr<AudioEffectResult>>;
