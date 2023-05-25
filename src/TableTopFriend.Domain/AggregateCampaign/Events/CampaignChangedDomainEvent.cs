using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateCampaign.Events;

public record CampaignChangedDomainEvent(
    CampaignId Id,
    UserId UserId,
    Name Name,
    Description Description,
    IReadOnlyList<CharacterId> CharacterIds,
    IReadOnlyList<SessionId> SessionIds,
    DateTime UpdatedAt
) : IDomainEvent;
