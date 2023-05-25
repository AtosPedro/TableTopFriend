using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Domain.AggregateCharacter;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Infrastructure.Persistence.Interfaces;

namespace TableTopFriend.Infrastructure.Persistence.Repositories;

public class CharacterRepository : Repository<Character, CharacterId, Guid>, ICharacterRepository
{
    public CharacterRepository(
        IApplicationDbContext dbContext,
        IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<IEnumerable<Character>> GetAll(
        UserId userId,
        CancellationToken cancellationToken)
    {
        return await SearchAsNoTracking(ch => ch.UserId == userId, cancellationToken);
    }

    public async Task<Character?> GetById(
        CharacterId id,
        CancellationToken cancellationToken)
    {
        return await base.GetById(id, cancellationToken);
    }
}
