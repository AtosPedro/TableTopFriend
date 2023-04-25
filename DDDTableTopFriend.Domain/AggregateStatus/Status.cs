using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.Events;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateStatus;

public class Status : AggregateRoot<StatusId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public byte[]? Image { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public float Quantity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Status(StatusId id) : base(id) { }

    private Status(
        StatusId id,
        UserId userId,
        string name,
        string description,
        float quantity,
        DateTime createdAt) : base(id)
    {
        UserId = userId;
        Name = name;
        Description = description;
        Quantity = quantity;
        CreatedAt = createdAt;
    }

    public static Status Create(
        UserId userId,
        string name,
        string description,
        float quantity,
        DateTime createdAt)
    {
        var status = new Status(
            StatusId.CreateUnique(),
            userId,
            name,
            description,
            quantity,
            createdAt
        );

        status.AddDomainEvent(new StatusCreatedDomainEvent(
            StatusId.Create(status.Id.Value),
            status.Name,
            status.Description,
            status.Quantity,
            status.CreatedAt
        ));

        return status;
    }

    public void Update(
        string name,
        string description,
        float quantity,
        DateTime updatedAt)
    {
        Name = name;
        Description = description;
        Quantity = quantity;
        UpdatedAt = updatedAt;

        AddDomainEvent(new StatusCreatedDomainEvent(
            StatusId.Create(Id.Value),
            Name,
            Description,
            Quantity,
            UpdatedAt.Value
        ));
    }

    public void MarkToDelete(DateTime deletedAt)
    {
        AddDomainEvent(new StatusDeletedDomainEvent(
            StatusId.Create(Id.Value),
            UserId,
            deletedAt
        ));
    }

#pragma warning disable CS8618
    private Status()
    {
    }
#pragma warning restore CS8618
}
