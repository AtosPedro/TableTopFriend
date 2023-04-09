namespace DDDTableTopFriend.Domain.Common.Models;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : notnull
{
    protected IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(TId id) : base(id) { }

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    protected bool RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
}
