using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateCampaign.Events;

public record CampaignCreatedDomainEvent(
    CampaignId Id,
    UserId UserId,
    string Name,
    string Description,
    IReadOnlyList<CharacterId> CharacterIds,
    DateTime CreatedAt
) : IDomainEvent;
