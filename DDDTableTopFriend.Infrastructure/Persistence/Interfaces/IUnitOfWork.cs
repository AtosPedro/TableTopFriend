using System.Collections.Generic;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork
{
    Task Commit(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = new CancellationToken());
    Task Rollback();
}
