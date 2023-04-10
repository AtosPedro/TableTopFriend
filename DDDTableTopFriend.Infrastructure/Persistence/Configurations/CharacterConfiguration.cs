using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateCharacter.Entities;
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
        ConfigureSessionAudioEffectIdsTable(builder);
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

    private static void ConfigureSessionAudioEffectIdsTable(EntityTypeBuilder<Character> characterBuilder)
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
                .FindNavigation(nameof(Character.AudioEffectIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureCharacterSheetTable(EntityTypeBuilder<Character> characterBuilder)
    {
        characterBuilder.OwnsOne(ch => ch.CharacterSheet, characterSheetBuilder =>
        {
            characterSheetBuilder
                .ToTable("CharacterSheet");

            characterSheetBuilder
                .WithOwner()
                .HasForeignKey("CharacterId");

            characterSheetBuilder
                .HasKey(csh => csh.Id);

            characterSheetBuilder
                .Property(csh => csh.Id)
                .HasConversion(id => id.Value, value => CharacterSheetId.Create(value));

            characterSheetBuilder
                .Property(c => c.Name)
                .HasMaxLength(50);

            characterSheetBuilder
                .Property(c => c.Description)
                .HasMaxLength(200);

            characterBuilder
                .Metadata
                .FindNavigation(nameof(Character.CharacterSheet))!.SetPropertyAccessMode(PropertyAccessMode.Field);

            ConfigureCharacterSheetStatusIdsTable(characterSheetBuilder, characterBuilder);
            ConfigureCharacterSheetSkillIdsTable(characterSheetBuilder, characterBuilder);
        });
    }

    private static void ConfigureCharacterSheetStatusIdsTable(
        OwnedNavigationBuilder<Character, CharacterSheet> characterSheetBuilder,
        EntityTypeBuilder<Character> characterBuilder)
    {
        characterSheetBuilder.OwnsMany(csh => csh.StatusIds, statusIdsBuilder =>
        {
            statusIdsBuilder
                .ToTable("CharacterSheetStatusIds");

            statusIdsBuilder
                .HasKey("Id");

            statusIdsBuilder
                .WithOwner()
                .HasForeignKey("CharacterSheetId");

            statusIdsBuilder
                .Property(c => c.Value)
                .HasColumnName("StatusId")
                .ValueGeneratedNever();

            characterBuilder
                .Metadata
                .FindNavigation(nameof(CharacterSheet.StatusIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureCharacterSheetSkillIdsTable(
        OwnedNavigationBuilder<Character, CharacterSheet> builder,
        EntityTypeBuilder<Character> characterBuilder)
    {
        builder.OwnsMany(csh => csh.StatusIds, skillIdsBuilder =>
        {
            skillIdsBuilder
                .ToTable("CharacterSheetSkillIds");

            skillIdsBuilder
                .WithOwner()
                .HasForeignKey("CharacterSheetId");

            skillIdsBuilder
                .HasKey("Id");

            skillIdsBuilder
                .Property(c => c.Value)
                .HasColumnName("SkillId")
                .ValueGeneratedNever();

            characterBuilder
                .Metadata
                .FindNavigation(nameof(Character.CharacterSheet.SkillIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}
