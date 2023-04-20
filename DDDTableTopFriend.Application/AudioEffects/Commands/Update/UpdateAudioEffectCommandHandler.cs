using DDDTableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Update;

public class UpdateAudioEffectCommandHandler : IRequestHandler<UpdateAudioEffectCommand, ErrorOr<AudioEffectResult>>
{
    public Task<ErrorOr<AudioEffectResult>> Handle(
        UpdateAudioEffectCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
