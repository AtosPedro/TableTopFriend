using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateCampaign.Events;

public record PlayerJoinedCampaignDomainEvent(
    UserId UserId,
    CampaignId CampaignId,
    CharacterId CharacterId
) : IDomainEvent;
