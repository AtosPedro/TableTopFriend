namespace DDDTableTopFriend.Domain.Common.Models;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : notnull
{
    protected IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(TId id) : base(id) { }

#pragma warning disable CS8618
    protected AggregateRoot()
    {
    }
#pragma warning restore CS8618

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    protected bool RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
}
