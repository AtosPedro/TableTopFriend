namespace DDDTableTopFriend.Domain.Entities;

public class Session : Entity
{
    public Guid UserId { get; set; }
    public Guid CampaignId { get; set; }
    public string Name{ get; set; } = null!;
    public TimeSpan Duration { get; set; }
    public DateTime Date { get; set; }
    public List<Character> Characters { get; private set;} = new();
    public Campaign Campaign { get; set; } = new ();
}
