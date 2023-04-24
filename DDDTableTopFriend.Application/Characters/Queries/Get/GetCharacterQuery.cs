using DDDTableTopFriend.Application.Characters.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Characters.Queries.Get;

public record GetCharacterQuery(
    Guid CharacterId
) : IRequest<ErrorOr<CharacterResult>>;
