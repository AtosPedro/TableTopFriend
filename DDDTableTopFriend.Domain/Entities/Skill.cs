namespace DDDTableTopFriend.Domain.Entities;

public class Skill : Entity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Cost { get; set; } = null!;
}
