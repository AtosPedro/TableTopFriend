using DDDTableTopFriend.Application.Characters.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Characters.Commands.Create;

public record CreateCharacterCommand(
    Guid UserId,
    string Name,
    string Description,
    int Type,
    CharacterSheetDto CharacterSheet,
    List<Guid> AudioEffectIds
) : IRequest<ErrorOr<CharacterResult>>;
