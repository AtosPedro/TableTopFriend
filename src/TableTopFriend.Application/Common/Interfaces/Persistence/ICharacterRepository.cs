using System.Linq.Expressions;
using TableTopFriend.Domain.AggregateCharacter;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;

namespace TableTopFriend.Application.Common.Interfaces.Persistence;

public interface ICharacterRepository
{
    Task<IEnumerable<Character>> SearchAsNoTracking(Expression<Func<Character, bool>> predicate, CancellationToken cancellationToken);
    Task<Character?> GetById(CharacterId id, CancellationToken cancellationToken);
    Task<IEnumerable<Character>> GetAll(UserId userId, CancellationToken cancellationToken);
    Task<Character> Add(Character campaign, CancellationToken cancellationToken);
    Task<Character> Update(Character campaign);
    Task<Character> Remove(Character campaign);
}
