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
            .OwnsOne(audioEffect => audioEffect.Name)
            .Property(name => name.Value)
            .HasColumnName("Name")
            .HasMaxLength(50);

        audioEffectBuilder
            .OwnsOne(audioEffect => audioEffect.Description)
            .Property(description => description.Value)
            .HasColumnName("Description")
            .HasMaxLength(5000);

        audioEffectBuilder
            .OwnsOne(audioEffect => audioEffect.AudioLink)
            .Property(audioLink => audioLink.Value)
            .HasColumnName("AudioLink")
            .HasMaxLength(600);

        audioEffectBuilder
            .OwnsOne(audioEffect => audioEffect.Clip)
            .Property(clip => clip.Value)
            .HasColumnName("AudioClip");
    }
}
