using DDDTableTopFriend.Domain.AggregateAudioEffect;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTableTopFriend.Infrastructure.Persistence.Configurations;

public class AudioEffectConfiguration : IEntityTypeConfiguration<AudioEffect>
{
    public void Configure(EntityTypeBuilder<AudioEffect> builder)
    {
        ConfigureAudioEffectsTable(builder);
    }

    private static void ConfigureAudioEffectsTable(EntityTypeBuilder<AudioEffect> builder)
    {
        builder
            .ToTable("AudioEffects");
        builder
            .HasKey(au => au.Id);
        builder
            .Property(au => au.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => AudioEffectId.Create(value));
        builder
            .Property(au => au.Name)
            .HasMaxLength(50);
        builder
            .Property(au => au.Description)
            .HasMaxLength(200);
        builder
            .Property(au => au.AudioLink)
            .HasMaxLength(600);
        builder
            .Property(au => au.AudioClip);
    }
}
