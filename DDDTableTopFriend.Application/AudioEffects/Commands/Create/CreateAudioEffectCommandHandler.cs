using DDDTableTopFriend.Application.AudioEffects.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Create;

public class CreateAudioEffectCommandHandler : IRequestHandler<CreateAudioEffectCommand, ErrorOr<AudioEffectResult>>
{
    public Task<ErrorOr<AudioEffectResult>> Handle(
        CreateAudioEffectCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
