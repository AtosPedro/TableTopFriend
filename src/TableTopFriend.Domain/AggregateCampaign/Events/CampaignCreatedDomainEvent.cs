using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateCampaign.Events;

public record CampaignCreatedDomainEvent(
    CampaignId Id,
    UserId UserId,
    Name Name,
    Description Description,
    IReadOnlyList<CharacterId> CharacterIds,
    DateTime CreatedAt
) : IDomainEvent;
