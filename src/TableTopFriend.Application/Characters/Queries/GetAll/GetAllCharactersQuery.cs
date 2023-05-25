using TableTopFriend.Application.Characters.Common;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Characters.Queries.GetAll;

public record GetAllCharactersQuery(
    Guid UserId
) : IRequest<ErrorOr<IReadOnlyList<CharacterResult>>>;
