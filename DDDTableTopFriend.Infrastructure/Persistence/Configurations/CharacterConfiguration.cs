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
        ConfigureCharacterSheetTable(builder);
        ConfigureCharacterAudioEffectIdsTable(builder);
    }

    private static void ConfigureCharactersTable(EntityTypeBuilder<Character> characterBuilder)
    {
        characterBuilder
            .ToTable("Characters");

        characterBuilder
            .HasKey(ch => ch.Id);

        characterBuilder
            .Property(ch => ch.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => CharacterId.Create(value));

        characterBuilder
            .Property(ch => ch.Type)
            .HasConversion(type => (int)type, value => (CharacterType)value);

        characterBuilder
            .Property(ch => ch.Name)
            .HasMaxLength(50);

        characterBuilder
            .Property(ch => ch.Description)
            .HasMaxLength(1000);
    }

    private static void ConfigureCharacterAudioEffectIdsTable(EntityTypeBuilder<Character> characterBuilder)
    {
        characterBuilder.OwnsMany(m => m.AudioEffectIds, audioEffectIdsBuilder =>
        {
            audioEffectIdsBuilder
                .ToTable("CharacterAudioEffectIds");

            audioEffectIdsBuilder
                .WithOwner()
                .HasForeignKey("CharacterId");

            audioEffectIdsBuilder
                .HasKey("Id");

            audioEffectIdsBuilder
                .Property(c => c.Value)
                .HasColumnName("AudioEffectId")
                .ValueGeneratedNever();

            characterBuilder
                .Metadata
                .FindNavigation(nameof(Character.AudioEffectIds))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureCharacterSheetTable(EntityTypeBuilder<Character> characterBuilder)
    {
        characterBuilder.OwnsOne(c => c.CharacterSheet, characterSheetBuilder =>
        {
            characterSheetBuilder
                .ToTable("CharacterSheets");

            characterSheetBuilder
                .WithOwner()
                .HasForeignKey("CharacterId");

            characterSheetBuilder
                .HasKey("Id", "CharacterId");

            characterSheetBuilder
                .Property(csh => csh.Id)
                .HasColumnName("CharacterSheetId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => CharacterSheetId.Create(value)
                );

            characterSheetBuilder
                .Property(c => c.Name)
                .HasMaxLength(50);

            characterSheetBuilder
                .Property(c => c.Description)
                .HasMaxLength(200);

            characterSheetBuilder.OwnsMany(csh => csh.StatusIds, statusIdsBuilder =>
            {
                statusIdsBuilder
                    .ToTable("CharacterSheetStatusIds");

                statusIdsBuilder
                    .WithOwner()
                    .HasForeignKey("CharacterSheetId", "CharacterId");

                statusIdsBuilder.HasKey("Id");

                statusIdsBuilder
                    .Property(c => c.Value)
                    .ValueGeneratedNever()
                    .HasColumnName("StatusId");

                characterSheetBuilder
                    .Navigation(s => s.StatusIds)
                    .Metadata
                    .SetField("_statusIds");

                characterSheetBuilder
                    .Navigation(s => s.StatusIds)
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            characterSheetBuilder.OwnsMany(csh => csh.StatusIds, skillIdsBuilder =>
            {
                skillIdsBuilder
                    .ToTable("CharacterSheetSkillIds");

                skillIdsBuilder
                    .WithOwner()
                    .HasForeignKey("CharacterSheetId", "CharacterId");

                skillIdsBuilder.HasKey("Id");

                skillIdsBuilder
                    .Property(c => c.Value)
                    .ValueGeneratedNever()
                    .HasColumnName("SkillId");

                characterSheetBuilder
                    .Navigation(s => s.StatusIds)
                    .Metadata
                    .SetField("_skillIds");

                characterSheetBuilder
                    .Navigation(s => s.StatusIds)
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            characterBuilder?
                .Metadata?
                .FindNavigation(nameof(Character.CharacterSheet))!
                    .SetPropertyAccessMode(PropertyAccessMode.Property);
        });
    }
}
