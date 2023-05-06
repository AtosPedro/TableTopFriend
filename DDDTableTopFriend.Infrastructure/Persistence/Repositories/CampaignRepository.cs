using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.ValueObjects;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence.Repositories;

public class CampaignRepository : Repository<Campaign, CampaignId, Guid>, ICampaignRepository
{
    public CampaignRepository(
        IApplicationDbContext dbContext,
        IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<IEnumerable<Campaign>> GetAll(
        UserId userId,
        CancellationToken cancellationToken)
    {
        return await Search(w => w.UserId == userId, cancellationToken);
    }

    public async Task<Campaign?> GetByName(
        Name name,
        UserId userId,
        CancellationToken cancellationToken)
    {
        return (await SearchAsNoTracking(w => w.Name.Value == name.Value && w.UserId == userId, cancellationToken)).FirstOrDefault();
    }
}
