using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Domain.AggregateCampaign;
using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.ValueObjects;
using TableTopFriend.Infrastructure.Persistence.Interfaces;

namespace TableTopFriend.Infrastructure.Persistence.Repositories;

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
