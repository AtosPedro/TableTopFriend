namespace TableTopFriend.Domain.Common.Models;

public interface IDomainEventHolder
{
    void AddDomainEvent(IDomainEvent domainEvent);
    List<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
}
