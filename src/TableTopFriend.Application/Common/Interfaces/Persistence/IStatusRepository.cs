using System.Linq.Expressions;
using TableTopFriend.Domain.AggregateStatus;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;

namespace TableTopFriend.Application.Common.Interfaces.Persistence;

public interface IStatusRepository
{
   Task<IEnumerable<Status>> SearchAsNoTracking(Expression<Func<Status, bool>> predicate, CancellationToken cancellationToken);
    Task<Status?> GetById(StatusId id, CancellationToken cancellationToken);
    Task<IEnumerable<Status>> GetAll(UserId userId, CancellationToken cancellationToken);
    Task<Status> Add(Status status, CancellationToken cancellationToken);
    Task<Status> Update(Status status);
    Task<Status> Remove(Status status);
}
