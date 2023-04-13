using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;
using MediatR;

namespace DDDTableTopFriend.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IPublisher _publisher;
    public UnitOfWork(IApplicationDbContext applicationDbContext, IPublisher mediator)
    {
        _applicationDbContext = applicationDbContext;
        _publisher = mediator;
    }

    public async Task Commit(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = new CancellationToken())
    {
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent, cancellationToken);
    }

    public async Task Rollback()
    {
        await Task.FromResult(true);
    }
}
