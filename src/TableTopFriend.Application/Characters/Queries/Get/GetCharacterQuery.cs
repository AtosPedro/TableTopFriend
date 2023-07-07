using TableTopFriend.Application.Characters.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Characters.Queries.Get;

public record GetCharacterQuery(
    Guid CharacterId
) : IRequest<ErrorOr<CharacterResult>>;
