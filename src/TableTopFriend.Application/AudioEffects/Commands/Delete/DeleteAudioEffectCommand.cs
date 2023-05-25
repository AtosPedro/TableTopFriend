using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.AudioEffects.Commands.Delete;

public record DeleteAudioEffectCommand(
    Guid Id
) : IRequest<ErrorOr<bool>>;
