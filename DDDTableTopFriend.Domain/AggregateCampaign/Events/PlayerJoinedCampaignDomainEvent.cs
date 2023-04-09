using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateCampaign.Events;

public record PlayerJoinedCampaignDomainEvent(
) : IDomainEvent;
