namespace DDDTableTopFriend.Domain.Entities;

public class AccessProfileItem : Entity
{
    public int AccessProfileId { get; set; }
    public string Name { get; set; } = null!;
    public AccessProfile AccessProfile { get; private set; } = null!;
}
