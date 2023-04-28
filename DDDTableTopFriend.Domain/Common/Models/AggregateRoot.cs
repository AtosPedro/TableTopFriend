using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.Common.Models;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId> , IDomainEventHolder
    where TId : AggregateRootId<TIdType>
{
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    private readonly List<IDomainEvent> _domainEvents = new();

    public new AggregateRootId<TIdType> Id { get; protected set; }

    protected AggregateRoot(TId id)
    {
        Id = id;
    }

    public AggregateRootId<TIdType> GetId()
    {
        return Id;
    }

#pragma warning disable CS8618
    protected AggregateRoot()
    {
    }
#pragma warning restore CS8618

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
    public List<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();
}
