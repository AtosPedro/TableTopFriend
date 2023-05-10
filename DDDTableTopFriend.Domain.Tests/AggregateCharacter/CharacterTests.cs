using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.ValueObjects;
using Moq;
using NUnit.Framework;

namespace DDDTableTopFriend.Domain.Tests.AggregateCharacter;

[TestFixture]
public class CharacterTests
{
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();
    private readonly IDateTimeProvider _dateTimeProvider;

    public CharacterTests()
    {
        var mockDate = DateTime.Parse("06/05/2023 00:00:00");
        _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(
            mockDate
        );
        _dateTimeProvider = _dateTimeProviderMock.Object;
    }

    [Test]
    public void Create_Character_Should_Return_Valid_Character()
    {
        UserId userId = UserId.CreateUnique();
        const string name = "character 1";
        const string description = "character's 1 desc";
        const CharacterType type = CharacterType.Player;
        List<AudioEffectId> audioEffectIds = new() { AudioEffectId.CreateUnique() };
        List<StatusId> statusIds = new() { StatusId.CreateUnique() };
        List<SkillId> skillId = new() { SkillId.CreateUnique() };
        const string sheetName = "character 1 sheet";
        const string sheetDescription = "character's 1 sheet desc";
        DateTime createdAt = _dateTimeProvider.UtcNow;

        Name nameVo = Name.Create(name).Value;
        Description descriptionVo = Description.Create(description).Value;
        Name sheetNameVo = Name.Create(sheetName).Value;
        Description sheetDescriptionVo = Description.Create(sheetDescription).Value;

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
        ).Value;

        Assert.Multiple(() =>
        {
            Assert.That(character.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(character.UserId, Is.EqualTo(userId));
            Assert.That(character.Name, Is.EqualTo(nameVo));
            Assert.That(character.Description, Is.EqualTo(descriptionVo));
            Assert.That(character.Type, Is.EqualTo(type));
            Assert.That(character.CreatedAt, Is.EqualTo(createdAt));
            Assert.That(character.AudioEffectIds, Is.EqualTo(audioEffectIds));
            Assert.That(character.CharacterSheet.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(character.CharacterSheet.Name, Is.EqualTo(sheetNameVo));
            Assert.That(character.CharacterSheet.Description, Is.EqualTo(sheetDescriptionVo));
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
        const CharacterType type = CharacterType.Player;
        List<AudioEffectId> audioEffectIds = new() { AudioEffectId.CreateUnique() };
        List<StatusId> statusIds = new() { StatusId.CreateUnique() };
        List<SkillId> skillId = new() { SkillId.CreateUnique() };
        const string sheetName = "character 1 sheet";
        const string sheetDescription = "character's 1 sheet desc";
        DateTime createdAt = _dateTimeProvider.UtcNow;

        UserId userIdUpdated = UserId.CreateUnique();
        const string nameUpdated = "character 1 Updated";
        const string descriptionUpdated = "character's 1 desc Updated";
        const CharacterType typeUpdated = CharacterType.Npc;
        List<AudioEffectId> audioEffectIdsUpdated = new() { AudioEffectId.CreateUnique() };
        List<StatusId> statusIdsUpdated = new() { StatusId.CreateUnique() };
        List<SkillId> skillIdUpdated = new() { SkillId.CreateUnique() };
        const string sheetNameUpdated = "character 1 sheet Updated";
        const string sheetDescriptionUpdated = "character's 1 sheet desc Updated";

        Name nameUpdatedVo = Name.Create(nameUpdated).Value;
        Description descriptionUpdatedVo = Description.Create(descriptionUpdated).Value;
        Name sheetNameUpdatedVo = Name.Create(sheetNameUpdated).Value;
        Description sheetDescriptionUpdatedVo = Description.Create(sheetDescriptionUpdated).Value;

        DateTime updatedAt = _dateTimeProvider.UtcNow;

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
        ).Value;

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
            Assert.That(character.Name, Is.EqualTo(nameUpdatedVo));
            Assert.That(character.Description, Is.EqualTo(descriptionUpdatedVo));
            Assert.That(character.Type, Is.EqualTo(typeUpdated));
            Assert.That(character.UpdatedAt, Is.EqualTo(updatedAt));
            Assert.That(character.AudioEffectIds, Is.EqualTo(audioEffectIdsUpdated));
            Assert.That(character.CharacterSheet.Id.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(character.CharacterSheet.Name, Is.EqualTo(sheetNameUpdatedVo));
            Assert.That(character.CharacterSheet.Description, Is.EqualTo(sheetDescriptionUpdatedVo));
            Assert.That(character.CharacterSheet.StatusIds, Is.EqualTo(statusIdsUpdated));
            Assert.That(character.CharacterSheet.SkillIds, Is.EqualTo(skillIdUpdated));
        });
    }
}
