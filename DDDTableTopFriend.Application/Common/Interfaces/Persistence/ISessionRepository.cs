using System.Linq.Expressions;
using DDDTableTopFriend.Domain.AggregateSession;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface ISessionRepository
{
    Task<IEnumerable<Session>> Search(Expression<Func<Session, bool>> predicate, CancellationToken cancellationToken);
    Task<Session?> GetById(SessionId id, CancellationToken cancellationToken);
    Task<IEnumerable<Session>> GetAll(UserId userId, CancellationToken cancellationToken);
    Task<Session> Add(Session session, CancellationToken cancellationToken);
    Task<Session> Update(Session session);
    Task<Session> Remove(Session session);
}
