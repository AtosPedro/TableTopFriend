using DDDTableTopFriend.Domain.AggregateAudioEffect;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTableTopFriend.Infrastructure.Persistence.Configurations;

public class AudioEffectConfiguration : IEntityTypeConfiguration<AudioEffect>
{
    public void Configure(EntityTypeBuilder<AudioEffect> builder)
    {
        ConfigureAudioEffectsTable(builder);
    }

    private static void ConfigureAudioEffectsTable(EntityTypeBuilder<AudioEffect> audioEffectBuilder)
    {
        audioEffectBuilder
            .ToTable("AudioEffects");

        audioEffectBuilder
            .HasKey(audioEffect => audioEffect.Id);

        audioEffectBuilder
            .Property(audioEffect => audioEffect.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => AudioEffectId.Create(value));

        audioEffectBuilder
            .Property(audioEffect => audioEffect.UserId)
            .HasConversion(id => id.Value, value => UserId.Create(value));

        audioEffectBuilder
            .Property(audioEffect => audioEffect.Name)
            .HasMaxLength(50);

        audioEffectBuilder
            .Property(audioEffect => audioEffect.Description)
            .HasMaxLength(200);

        audioEffectBuilder
            .Property(audioEffect => audioEffect.AudioLink)
            .HasMaxLength(600);

        audioEffectBuilder
            .Property(audioEffect => audioEffect.AudioClip);
    }
}
