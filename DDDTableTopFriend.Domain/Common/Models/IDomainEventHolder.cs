namespace DDDTableTopFriend.Domain.Common.Models;

public interface IDomainEventHolder
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    bool RemoveDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}

