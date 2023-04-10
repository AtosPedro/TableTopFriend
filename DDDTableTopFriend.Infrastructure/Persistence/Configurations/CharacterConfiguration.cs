using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTableTopFriend.Infrastructure.Persistence.Configurations;

public class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        ConfigureCharactersTable(builder);
        ConfigureSessionAudioEffectIdsTable(builder);
    }

    private static void ConfigureCharactersTable(EntityTypeBuilder<Character> builder)
    {
        builder.ToTable("Characters");
        builder.HasKey(ch=>ch.Id);
        builder.Property(ch => ch.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => CharacterId.Create(value));

        builder.Property(ch => ch.Type).HasConversion(type => (int)type, value => (CharacterType)value);
        builder.Property(ch => ch.Name).HasMaxLength(50);
        builder.Property(ch => ch.Description).HasMaxLength(1000);
    }

    private static void ConfigureSessionAudioEffectIdsTable(EntityTypeBuilder<Character> builder)
    {
        builder.OwnsMany(m => m.AudioEffectIds, ch =>
        {
            ch.ToTable("CharacterAudioEffectIds");
            ch.WithOwner().HasForeignKey("CharacterId");
            ch.HasKey("Id");
            ch.Property(c => c.Value)
                .HasColumnName("AudioEffectId")
                .ValueGeneratedNever();

            builder
                .Metadata
                .FindNavigation(nameof(Character.AudioEffectIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}
