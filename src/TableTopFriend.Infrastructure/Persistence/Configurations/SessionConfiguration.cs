using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateSession;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableTopFriend.Infrastructure.Persistence.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        ConfigureSessionsTable(builder);
        ConfigureSessionCharacterIdsTable(builder);
        ConfigureSessionAudioEffectIdsTable(builder);
    }

    private static void ConfigureSessionsTable(EntityTypeBuilder<Session> sessionBuilder)
    {
        sessionBuilder
            .ToTable("Sessions");

        sessionBuilder
            .HasKey(se => se.Id);

        sessionBuilder
            .Property(se => se.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => SessionId.Create(value));

        sessionBuilder
            .Property(se => se.CampaignId)
            .ValueGeneratedNever()
            .HasConversion(campaignId => campaignId.Value, value => CampaignId.Create(value));

        sessionBuilder
            .Property(se => se.UserId)
            .ValueGeneratedNever()
            .HasConversion(userId => userId.Value, value => UserId.Create(value));

        sessionBuilder
            .OwnsOne(s => s.Name)
            .Property(name => name.Value)
            .HasColumnName("Name")
            .HasMaxLength(50);

        sessionBuilder
            .OwnsOne(s => s.Description)
            .Property(description => description.Value)
            .HasColumnName("Description")
            .HasMaxLength(5000);
    }

    private static void ConfigureSessionCharacterIdsTable(EntityTypeBuilder<Session> sessionBuilder)
    {
        sessionBuilder.OwnsMany(session => session.CharacterIds, characterIdsBuilder =>
        {
            characterIdsBuilder
                .ToTable("SessionCharacterIds");

            characterIdsBuilder
                .WithOwner()
                .HasForeignKey("SessionId");

            characterIdsBuilder
                .HasKey("Id");

            characterIdsBuilder
                .Property(c => c.Value)
                .HasColumnName("CharacterId")
                .ValueGeneratedNever();

            sessionBuilder
                .Metadata
                .FindNavigation(nameof(Session.CharacterIds))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureSessionAudioEffectIdsTable(EntityTypeBuilder<Session> sessionBuilder)
    {
        sessionBuilder.OwnsMany(session => session.AudioEffectIds, audioEffectIds =>
        {
            audioEffectIds
                .ToTable("SessionAudioEffectIds");

            audioEffectIds
                .WithOwner()
                .HasForeignKey("SessionId");

            audioEffectIds
                .HasKey("Id");

            audioEffectIds
                .Property(c => c.Value)
                .HasColumnName("AudioEffectId")
                .ValueGeneratedNever();

            sessionBuilder
                .Metadata
                .FindNavigation(nameof(Session.AudioEffectIds))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}
