using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateCampaign.Events;

public record PlayerJoinedCampaignDomainEvent(
CampaignId CampaignId,
CharacterId CharacterId) : IDomainEvent;
