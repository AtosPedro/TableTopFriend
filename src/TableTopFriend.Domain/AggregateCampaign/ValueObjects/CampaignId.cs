using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateCampaign.ValueObjects;

public sealed class CampaignId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private CampaignId(Guid value)
    {
        Value = value;
    }

    public static CampaignId CreateUnique() => new(Guid.NewGuid());
    public static CampaignId Create(Guid id) => new(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private CampaignId()
    {
    }
#pragma warning restore CS8618
}
