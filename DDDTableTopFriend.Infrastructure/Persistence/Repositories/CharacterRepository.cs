using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence.Repositories;

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
