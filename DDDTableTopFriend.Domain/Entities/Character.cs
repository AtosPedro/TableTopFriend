namespace DDDTableTopFriend.Domain.Entities;

public class Character : Entity
{
    public Guid UserId { get; set; }
    public Guid? CharacterSheetId { get; set; }
    public Guid? CampaignId { get; set; }
    public string Name { get; set; } = null!;
    public CharacterType Type { get; set; } = CharacterType.Player;
    CharacterSheet? CharacterSheet { get; set;}
}
