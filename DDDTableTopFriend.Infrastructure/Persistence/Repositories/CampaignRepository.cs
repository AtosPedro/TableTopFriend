using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence;

public class CampaignRepository : Repository<Campaign>, ICampaignRepository
{
    public CampaignRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Campaign>> GetAll(UserId userId, CancellationToken cancellationToken)
    {
        return await Search(w => w.UserId == userId, cancellationToken);
    }

    public async Task<Campaign?> GetById(CampaignId id, CancellationToken cancellationToken)
    {
        return await EntityDbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<Campaign?> GetByName(string name, CancellationToken cancellationToken)
    {
        return (await Search(w => w.Name == name, cancellationToken)).FirstOrDefault();
    }
}
