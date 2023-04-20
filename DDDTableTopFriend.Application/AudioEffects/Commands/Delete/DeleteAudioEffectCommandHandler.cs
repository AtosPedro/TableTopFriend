using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Delete;

public class DeleteAudioEffectCommandHandler : IRequestHandler<DeleteAudioEffectCommand, ErrorOr<bool>>
{
    public Task<ErrorOr<bool>> Handle(
        DeleteAudioEffectCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
