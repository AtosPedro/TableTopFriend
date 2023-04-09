using System.Linq.Expressions;
using DDDTableTopFriend.Domain.AggregateCampaign;

namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface ICampaignRepository
{
    Task<IEnumerable<Campaign>> Search(Expression<Func<Campaign, bool>> predicate, CancellationToken cancellationToken);
    Task<Campaign?> GetById(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Campaign>> GetAll(Guid userId, CancellationToken cancellationToken);
    Task<Campaign?> GetByName(string name, CancellationToken cancellationToken);
    Task<Campaign> Add(Campaign campaign, CancellationToken cancellationToken);
    Task<Campaign> Update(Campaign campaign);
    Task<Campaign> Remove(Campaign campaign);
}
