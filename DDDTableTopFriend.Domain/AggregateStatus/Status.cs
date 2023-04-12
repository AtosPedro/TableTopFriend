using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateStatus;

public class Status : AggregateRoot<StatusId, Guid>
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set;} = null!;
    public float Quantity { get; private set;}
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set;}

    public Status(StatusId id) : base(id) { }

    private Status(
        StatusId id,
        string name,
        string description,
        float quantity,
        DateTime? createdAt,
        DateTime? updatedAt) : base(id)
    {
        Name = name;
        Description = description;
        Quantity = quantity;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Status Create(
        string name,
        string description,
        float quantity,
        DateTime? createdAt)
    {
        return new (
            StatusId.CreateUnique(),
            name,
            description,
            quantity,
            createdAt,
            null
        );
    }

#pragma warning disable CS8618
    private Status()
    {
    }
#pragma warning restore CS8618
}
