using DDDTableTopFriend.Application.Characters.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Characters.Queries.GetAll;

public record GetAllCharactersQuery(
    Guid UserId
) : IRequest<ErrorOr<IReadOnlyList<CharacterResult>>>;
