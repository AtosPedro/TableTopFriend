using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Characters.Commands.CastSkill;

public class CharacterCastedSkillCommandHandler : IRequestHandler<CharacterCastedSkillCommand, ErrorOr<bool>>
{
    public CharacterCastedSkillCommandHandler()
    {
    }

    public Task<ErrorOr<bool>> Handle(CharacterCastedSkillCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
