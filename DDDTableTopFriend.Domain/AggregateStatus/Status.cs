using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateStatus;

public class Status : AggregateRoot<StatusId, Guid>
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public float Quantity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Status(StatusId id) : base(id) { }

    private Status(
        StatusId id,
        string name,
        string description,
        float quantity,
        DateTime createdAt) : base(id)
    {
        Name = name;
        Description = description;
        Quantity = quantity;
        CreatedAt = createdAt;
    }

    public static Status Create(
        string name,
        string description,
        float quantity,
        DateTime createdAt)
    {
        return new Status(
            StatusId.CreateUnique(),
            name,
            description,
            quantity,
            createdAt
        );
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
        CreatedAt = updatedAt;
    }

    public void MarkToDelete(DateTime deletedAt)
    {

    }

#pragma warning disable CS8618
    private Status()
    {
    }
#pragma warning restore CS8618
}
