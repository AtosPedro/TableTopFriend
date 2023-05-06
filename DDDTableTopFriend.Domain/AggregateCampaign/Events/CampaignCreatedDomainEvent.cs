using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCampaign.Events;

public record CampaignCreatedDomainEvent(
    CampaignId Id,
    UserId UserId,
    Name Name,
    Description Description,
    IReadOnlyList<CharacterId> CharacterIds,
    DateTime CreatedAt
) : IDomainEvent;
