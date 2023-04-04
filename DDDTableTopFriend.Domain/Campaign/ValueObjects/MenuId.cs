using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.Campaign.ValueObjects;

public sealed class CampaignId : ValueObject
{
    public Guid Value { get; set; }

    private CampaignId(Guid value)
    {
        Value = value;
    }

    public static CampaignId CreateUnique() => new (Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
