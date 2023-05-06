using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.Common.ValueObjects;

namespace DDDTableTopFriend.Domain.AggregateCampaign.Events;

public record CampaignChangedDomainEvent(
    CampaignId Id,
    UserId UserId,
    Name Name,
    Description Description,
    IReadOnlyList<CharacterId> CharacterIds,
    IReadOnlyList<SessionId> SessionIds,
    DateTime UpdatedAt
) : IDomainEvent;
