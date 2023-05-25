using TableTopFriend.Domain.Common.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace TableTopFriend.Infrastructure.Persistence.Interceptors;

public sealed class DomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var messages = dbContext
            .ChangeTracker
            .Entries<IDomainEventHolder>()
            .Select(x => x.Entity)
            .SelectMany(x =>
            {
                var domainEvents = x.GetDomainEvents();
                x.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            })
            .ToList();

        dbContext.Set<OutboxMessage>().AddRange(messages);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
