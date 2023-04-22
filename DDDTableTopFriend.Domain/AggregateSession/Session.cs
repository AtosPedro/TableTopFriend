﻿using System.Collections.Generic;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession.Events;

namespace DDDTableTopFriend.Domain.AggregateSession;

public class Session : AggregateRoot<SessionId, Guid>
{
    public UserId UserId { get; private set; } = null!;
    public CampaignId CampaignId { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public DateTime DateTime { get; private set; }
    public TimeSpan Duration { get; private set; }
    public IReadOnlyList<CharacterId> CharacterIds => _characterIds.AsReadOnly();
    public IReadOnlyList<AudioEffectId> AudioEffectIds => _audioEffectIds.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<CharacterId> _characterIds = new();
    private readonly List<AudioEffectId> _audioEffectIds = new();

    public Session(SessionId id) : base(id) { }

    private Session(
        SessionId id,
        UserId userId,
        CampaignId campaignId,
        string name,
        DateTime dateTime,
        List<CharacterId> characterIds,
        List<AudioEffectId> audioEffectIds,
        DateTime createdAt) : base(id)
    {
        CampaignId = campaignId;
        UserId = userId;
        Name = name;
        DateTime = dateTime;
        CreatedAt = createdAt;
        _characterIds = characterIds;
        _audioEffectIds = audioEffectIds;
    }

    public static Session Create(
        UserId userId,
        CampaignId campaignId,
        string name,
        DateTime dateTime,
        DateTime createdAt)
    {
        var session = new Session(
            SessionId.CreateUnique(),
            userId,
            campaignId,
            name,
            dateTime,
            new List<CharacterId>(),
            new List<AudioEffectId>(),
            createdAt
        );

        session.AddDomainEvent(new SessionScheduledDomainEvent(
            session.UserId,
            session.CampaignId,
            SessionId.Create(session.Id.Value)
        ));

        return session;
    }

    public void Update(
        string name,
        DateTime dateTime,
        TimeSpan duration,
        DateTime updatedAt)
    {
        Name = name;
        DateTime = dateTime;
        Duration = duration;
        UpdatedAt = updatedAt;

        AddDomainEvent(new SessionChangedDomainEvent(
            UserId,
            CampaignId,
            SessionId.Create(Id.Value)
        ));
    }

    public void MarkToDelete(DateTime utcNow)
    {
    }

#pragma warning disable CS8618
    private Session()
    {
    }
#pragma warning restore CS8618
}
