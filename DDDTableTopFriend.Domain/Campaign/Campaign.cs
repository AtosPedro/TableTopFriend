using DDDTableTopFriend.Domain.Campaign.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.Campaign;

public sealed class Campaign : AggregateRoot<CampaignId>
{
    public string Name { get; }
    public string Description { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

    public Campaign(CampaignId id) : base(id)
    {
    }
}
