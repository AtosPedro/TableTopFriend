using System.Linq.Expressions;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface ICampaignRepository
{
    Task<IEnumerable<Campaign>> Search(Expression<Func<Campaign, bool>> predicate, CancellationToken cancellationToken);
    Task<Campaign?> GetById(CampaignId id, CancellationToken cancellationToken);
    Task<IEnumerable<Campaign>> GetAll(UserId userId, CancellationToken cancellationToken);
    Task<Campaign?> GetByName(string name, CancellationToken cancellationToken);
    Task<Campaign> Add(Campaign campaign, CancellationToken cancellationToken);
    Task<Campaign> Update(Campaign campaign);
    Task<Campaign> Remove(Campaign campaign);
}
