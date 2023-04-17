using System.Linq.Expressions;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface ICampaignRepository
{
    Task<IEnumerable<Campaign>> Search(Expression<Func<Campaign, bool>> predicate, CancellationToken cancellationToken);
    Task<Campaign?> GetById(AggregateRootId<Guid> id, CancellationToken cancellationToken);
    Task<IEnumerable<Campaign>> GetAll(UserId userId, CancellationToken cancellationToken);
    Task<Campaign?> GetByName(string name, UserId userId, CancellationToken cancellationToken);
    Task<Campaign> Add(Campaign campaign, CancellationToken cancellationToken);
    Task<Campaign> Update(Campaign campaign);
    Task<Campaign> Remove(Campaign campaign);
}
