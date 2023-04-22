using System.Collections.Generic;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    Task Commit(
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = new CancellationToken());
    Task<T> Execute<T>(
        Func<CancellationToken,Task<T>> getData,
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = new CancellationToken());
    Task Rollback();
}
