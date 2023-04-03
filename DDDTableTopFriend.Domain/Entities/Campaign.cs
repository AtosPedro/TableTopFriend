namespace DDDTableTopFriend.Domain.Entities;


public class Campaign : Entity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<Character> Characters { get; private set; } = new();
    public List<Session> Sessions { get; private set; } = new();
}
