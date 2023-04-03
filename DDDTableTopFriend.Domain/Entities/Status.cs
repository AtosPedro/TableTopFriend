namespace DDDTableTopFriend.Domain.Entities;

public class Status : Entity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
