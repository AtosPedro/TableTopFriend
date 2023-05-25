using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateSession;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Infrastructure.Persistence.Interfaces;

namespace TableTopFriend.Infrastructure.Persistence.Repositories;

public class SessionRepository : Repository<Session, SessionId, Guid>, ISessionRepository
{
    public SessionRepository(
        IApplicationDbContext dbContext,
        IUnitOfWork unitOfWork) : base(dbContext, unitOfWork) { }

    public async Task<IEnumerable<Session>> GetAll(
        UserId userId,
        CampaignId campaignId,
        CancellationToken cancellationToken)
    {
        return await base.SearchAsNoTracking(w => w.UserId == userId && w.CampaignId == campaignId, cancellationToken);
    }

    public async Task<Session?> GetById(
        SessionId id,
        CancellationToken cancellationToken)
    {
        return await base.GetById(id, cancellationToken);
    }
}
