using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using NUnit.Framework;

namespace DDDTableTopFriend.Domain.Tests.AggregateCharacter;

[TestFixture]
public class CharacterTests
{
    [Test]
    public void Create_Character_Should_Return_Valid_Character()
    {
        UserId userId = UserId.CreateUnique();
        const string name = "character 1";
        const string description = "character's 1 desc";
        CharacterType type = CharacterType.Player;
        List<AudioEffectId> audioEffectIds = new() { AudioEffectId.CreateUnique() };
        List<StatusId> statusIds = new() { StatusId.CreateUnique() };
        List<SkillId> skillId = new() { SkillId.CreateUnique() };
        const string sheetName = "character 1 sheet";
        const string sheetDescription = "character's 1 sheet desc";        
        DateTime createdAt = DateTime.UtcNow;

        var character = Character.Create(
            userId,
            name,
            description,
            type,
            audioEffectIds,
            sheetName,
            sheetDescription,
            statusIds,
            skillId,
            createdAt
        );

        Assert.Multiple(() =>
        {
            Assert.That(character.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(character.UserId, Is.EqualTo(userId));
            Assert.That(character.Name, Is.EqualTo(name));
            Assert.That(character.Description, Is.EqualTo(description));
            Assert.That(character.Type, Is.EqualTo(type));
            Assert.That(character.CreatedAt, Is.EqualTo(createdAt));
            Assert.That(character.AudioEffectIds, Is.EqualTo(audioEffectIds));
            Assert.That(character.CharacterSheet.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(character.CharacterSheet.Name, Is.EqualTo(sheetName));
            Assert.That(character.CharacterSheet.Description, Is.EqualTo(sheetDescription));
            Assert.That(character.CharacterSheet.StatusIds, Is.EqualTo(statusIds));
            Assert.That(character.CharacterSheet.SkillIds, Is.EqualTo(skillId));
        });
    }

    [Test]
    public void Update_Character_Should_Update_Character_Instance()
    {
        UserId userId = UserId.CreateUnique();
        const string name = "character 1";
        const string description = "character's 1 desc";
        CharacterType type = CharacterType.Player;
        List<AudioEffectId> audioEffectIds = new() { AudioEffectId.CreateUnique() };
        List<StatusId> statusIds = new() { StatusId.CreateUnique() };
        List<SkillId> skillId = new() { SkillId.CreateUnique() };
        const string sheetName = "character 1 sheet";
        const string sheetDescription = "character's 1 sheet desc";        
        DateTime createdAt = DateTime.UtcNow;

        UserId userIdUpdated = UserId.CreateUnique();
        const string nameUpdated = "character 1 Updated";
        const string descriptionUpdated = "character's 1 desc Updated";
        CharacterType typeUpdated = CharacterType.Npc;
        List<AudioEffectId> audioEffectIdsUpdated = new() { AudioEffectId.CreateUnique() };
        List<StatusId> statusIdsUpdated = new() { StatusId.CreateUnique() };
        List<SkillId> skillIdUpdated = new() { SkillId.CreateUnique() };
        const string sheetNameUpdated = "character 1 sheet Updated";
        const string sheetDescriptionUpdated = "character's 1 sheet desc Updated";        
        DateTime updatedAt = DateTime.UtcNow;

        var character = Character.Create(
            userId,
            name,
            description,
            type,
            audioEffectIds,
            sheetName,
            sheetDescription,
            statusIds,
            skillId,
            createdAt
        );

        character.ClearDomainEvents();

        character.Update(
            nameUpdated,
            descriptionUpdated,
            typeUpdated,
            audioEffectIdsUpdated,
            sheetNameUpdated,
            sheetDescriptionUpdated,
            statusIdsUpdated,
            skillIdUpdated,
            updatedAt
        );

        Assert.Multiple(() =>
        {
            Assert.That(character.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(character.UserId, Is.EqualTo(userId));
            Assert.That(character.Name, Is.EqualTo(nameUpdated));
            Assert.That(character.Description, Is.EqualTo(descriptionUpdated));
            Assert.That(character.Type, Is.EqualTo(typeUpdated));
            Assert.That(character.UpdatedAt, Is.EqualTo(updatedAt));
            Assert.That(character.AudioEffectIds, Is.EqualTo(audioEffectIdsUpdated));
            Assert.That(character.CharacterSheet.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(character.CharacterSheet.Name, Is.EqualTo(sheetNameUpdated));
            Assert.That(character.CharacterSheet.Description, Is.EqualTo(sheetDescriptionUpdated));
            Assert.That(character.CharacterSheet.StatusIds, Is.EqualTo(statusIdsUpdated));
            Assert.That(character.CharacterSheet.SkillIds, Is.EqualTo(skillIdUpdated));
        });
    }
}
