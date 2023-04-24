using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence.Repositories;

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
