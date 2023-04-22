using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
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

    public async Task Commit(
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent, cancellationToken);
    }

    public async Task<T> Execute<T>(
        Func<CancellationToken,Task<T>> getData,
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var result = await getData(cancellationToken);
            await Commit(domainEvents, cancellationToken);
            return result;
        }
        catch
        {
            await Rollback();
            throw;
        }
    }

    public async Task Rollback()
    {
        await Task.FromResult(true);
    }
}
