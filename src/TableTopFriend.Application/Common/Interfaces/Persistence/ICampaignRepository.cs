using System.Linq.Expressions;
using TableTopFriend.Domain.AggregateCampaign;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Application.Common.Interfaces.Persistence;

public interface ICampaignRepository
{
    Task<IEnumerable<Campaign>> SearchAsNoTracking(Expression<Func<Campaign, bool>> predicate, CancellationToken cancellationToken);
    Task<Campaign?> GetById(AggregateRootId<Guid> id, CancellationToken cancellationToken);
    Task<IEnumerable<Campaign>> GetAll(UserId userId, CancellationToken cancellationToken);
    Task<Campaign?> GetByName(Name name, UserId userId, CancellationToken cancellationToken);
    Task<Campaign> Add(Campaign campaign, CancellationToken cancellationToken);
    Task<Campaign> Update(Campaign campaign);
    Task<Campaign> Remove(Campaign campaign);
}
