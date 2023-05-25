using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.AggregateStatus.Events;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.ValueObjects;
using ErrorOr;

namespace TableTopFriend.Domain.AggregateStatus;

public class Status : AggregateRoot<StatusId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public byte[]? Image { get; private set; }
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public float Quantity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Status(StatusId id) : base(id) { }

    private Status(
        StatusId id,
        UserId userId,
        Name name,
        Description description,
        float quantity,
        DateTime createdAt) : base(id)
    {
        UserId = userId;
        Name = name;
        Description = description;
        Quantity = quantity;
        CreatedAt = createdAt;
    }

    public static ErrorOr<Status> Create(
        UserId userId,
        string nameStr,
        string descriptionStr,
        float quantity,
        DateTime createdAt)
    {
        var errorList = new List<Error>();
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

        if (name.IsError)
            errorList.AddRange(name.Errors);

        if (description.IsError)
            errorList.AddRange(description.Errors);

        if (errorList.Any())
            return errorList;

        var status = new Status(
            StatusId.CreateUnique(),
            userId,
            name.Value,
            description.Value,
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

    public ErrorOr<Status> Update(
        string nameStr,
        string descriptionStr,
        float quantity,
        DateTime updatedAt)
    {
        var errorList = new List<Error>();
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);

        if (name.IsError)
            errorList.AddRange(name.Errors);

        if (description.IsError)
            errorList.AddRange(description.Errors);

        if (errorList.Any())
            return errorList;

        Name = name.Value ?? Name;
        Description = description.Value ?? Description;
        Quantity = quantity;
        UpdatedAt = updatedAt;

        AddDomainEvent(new StatusChangedDomainEvent(
            StatusId.Create(Id.Value),
            Name,
            Description,
            Quantity,
            UpdatedAt.Value
        ));

        return this;
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
