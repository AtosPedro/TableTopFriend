using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Characters.Commands.CastSkill;

public record CharacterCastedSkillCommand(

) : IRequest<ErrorOr<bool>>;
