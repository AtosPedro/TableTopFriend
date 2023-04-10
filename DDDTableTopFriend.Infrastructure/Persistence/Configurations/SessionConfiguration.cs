using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTableTopFriend.Infrastructure.Persistence.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        ConfigureSessionsTable(builder);
        ConfigureSessionCharacterIdsTable(builder);
        ConfigureSessionAudioEffectIdsTable(builder);
    }

    private static void ConfigureSessionsTable(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");

        builder.HasKey(se => se.Id);
        builder.Property(se => se.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => SessionId.Create(value));

        builder.Property(se => se.CampaignId)
            .ValueGeneratedNever()
            .HasConversion(campaignId => campaignId.Value, value => CampaignId.Create(value));
    }

    private static void ConfigureSessionCharacterIdsTable(EntityTypeBuilder<Session> builder)
    {
        builder.OwnsMany(m => m.CharacterIds, ch =>
        {
            ch.ToTable("SessionCharacterIds");
            ch.WithOwner().HasForeignKey("SessionId");
            ch.HasKey("Id");
            ch.Property(c => c.Value)
                .HasColumnName("CharacterId")
                .ValueGeneratedNever();

            builder
                .Metadata
                .FindNavigation(nameof(Session.CharacterIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureSessionAudioEffectIdsTable(EntityTypeBuilder<Session> builder)
    {
        builder.OwnsMany(m => m.AudioEffectIds, ch =>
        {
            ch.ToTable("SessionAudioEffectIds");
            ch.WithOwner().HasForeignKey("SessionId");
            ch.HasKey("Id");
            ch.Property(c => c.Value)
                .HasColumnName("AudioEffectId")
                .ValueGeneratedNever();

            builder
                .Metadata
                .FindNavigation(nameof(Session.AudioEffectIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}
