using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateStatus;

public class Status : AggregateRoot<StatusId>
{
    public string Name { get; }
    public string Description { get; }
    public float Quantity { get; }
    public DateTime? CreatedAt { get; }
    public DateTime? UpdatedAt { get; }

    public Status(
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
}
