using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence;

public class CampaignRepository : Repository<Campaign>, ICampaignRepository
{
    public CampaignRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Campaign>> GetAll(Guid userId, CancellationToken cancellationToken)
    {
        return await Search(w => w.UserId.Value == userId, cancellationToken);
    }

    public async Task<Campaign?> GetByName(string name, CancellationToken cancellationToken)
    {
        return (await Search(w => w.Name == name, cancellationToken)).FirstOrDefault();
    }
}
