namespace DDDTableTopFriend.Domain.Entities;

public class CharacterSheet
{
    public Guid UserId { get; set; }
    public Guid CharacterId { get; set; }
    public string Title { get; set; } = null!;
    public List<Skill> Skills { get; private set; } = new();
    public List<Status> Status { get; private set; } = new();
}
