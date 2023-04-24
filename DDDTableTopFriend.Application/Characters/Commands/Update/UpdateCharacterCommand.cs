using DDDTableTopFriend.Application.Characters.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Characters.Commands.Update;

public record UpdateCharacterCommand(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    int Type,
    CharacterSheetDto CharacterSheet,
    List<Guid> AudioEffectIds
) : IRequest<ErrorOr<CharacterResult>>;
