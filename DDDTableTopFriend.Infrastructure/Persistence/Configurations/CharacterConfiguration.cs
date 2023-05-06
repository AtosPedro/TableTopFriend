using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateCharacter.Entities;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
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
            .Property(ch => ch.UserId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => UserId.Create(value));

        characterBuilder
            .Property(ch => ch.Type)
            .HasConversion(type => (int)type, value => (CharacterType)value)
            .HasComment("0 - Player, 1 - NPC, 2 - Enemy");

        characterBuilder
            .OwnsOne(ch => ch.Name)
            .Property(name => name.Value)
            .HasColumnName("Name")
            .HasMaxLength(50);

        characterBuilder
            .OwnsOne(ch => ch.Description)
            .Property(description => description.Value)
            .HasColumnName("Description")
            .HasMaxLength(5000);
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
                .ValueGeneratedNever()
                .HasColumnName(nameof(CharacterSheet.Id))
                .HasConversion(
                    id => id.Value,
                    value => CharacterSheetId.Create(value)
                );

            characterSheetBuilder
                .OwnsOne(ch => ch.Name)
                .Property(name => name.Value)
                .HasColumnName("Name")
                .HasMaxLength(50);

            characterSheetBuilder
                .OwnsOne(ch => ch.Description)
                .Property(description => description.Value)
                .HasColumnName("Description")
                .HasMaxLength(5000);

            characterSheetBuilder.OwnsMany(csh => csh.StatusIds, statusIdsBuilder =>
            {
                statusIdsBuilder
                    .ToTable("CharacterSheetStatusIds");

                statusIdsBuilder
                    .WithOwner()
                    .HasForeignKey(
                        nameof(CharacterSheetId),
                        nameof(CharacterId)
                    );

                statusIdsBuilder.HasKey(
                    nameof(CharacterSheetId),
                    nameof(CharacterId)
                );

                statusIdsBuilder
                    .Property(c => c.Value)
                    .ValueGeneratedNever()
                    .HasColumnName(nameof(StatusId));

                characterSheetBuilder
                    .Navigation(s => s.StatusIds)
                    .Metadata
                    .SetField("_statusIds");

                characterSheetBuilder
                    .Navigation(nameof(Character.CharacterSheet.StatusIds))
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            characterSheetBuilder.OwnsMany(csh => csh.SkillIds, skillIdsBuilder =>
            {
                skillIdsBuilder
                    .ToTable("CharacterSheetSkillIds");

                skillIdsBuilder
                    .WithOwner()
                    .HasForeignKey(
                        nameof(CharacterSheetId),
                        nameof(CharacterId)
                    );

                skillIdsBuilder
                    .HasKey(
                        nameof(CharacterSheetId),
                        nameof(CharacterId)
                    );

                skillIdsBuilder
                    .Property(c => c.Value)
                    .ValueGeneratedNever()
                    .HasColumnName(nameof(SkillId));

                characterSheetBuilder
                    .Navigation(s => s.SkillIds)
                    .Metadata
                    .SetField("_skillIds");

                characterSheetBuilder
                    .Navigation(nameof(Character.CharacterSheet.SkillIds))
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            characterBuilder?
                .Metadata?
                .FindNavigation(nameof(Character.CharacterSheet))!
                    .SetPropertyAccessMode(PropertyAccessMode.Property);
        });
    }
}
