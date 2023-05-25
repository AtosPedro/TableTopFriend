using TableTopFriend.Application.Characters.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Characters.Commands.Create;

public record CreateCharacterCommand(
    Guid UserId,
    string Name,
    string Description,
    int Type,
    CharacterSheetDto CharacterSheet,
    List<Guid> AudioEffectIds
) : IRequest<ErrorOr<CharacterResult>>;
