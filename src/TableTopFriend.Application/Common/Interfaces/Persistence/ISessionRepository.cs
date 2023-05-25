using System.Linq.Expressions;
using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateSession;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;

namespace TableTopFriend.Application.Common.Interfaces.Persistence;

public interface ISessionRepository
{
    Task<IEnumerable<Session>> SearchAsNoTracking(Expression<Func<Session, bool>> predicate, CancellationToken cancellationToken);
    Task<Session?> GetById(SessionId id, CancellationToken cancellationToken);
    Task<IEnumerable<Session>> GetAll(UserId userId, CampaignId campaignId, CancellationToken cancellationToken);
    Task<Session> Add(Session session, CancellationToken cancellationToken);
    Task<Session> Update(Session session);
    Task<Session> Remove(Session session);
}
