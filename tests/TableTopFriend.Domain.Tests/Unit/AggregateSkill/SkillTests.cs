using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.AggregateSkill;
using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.AggregateStatus.Events;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.ValueObjects;
using Moq;
using NUnit.Framework;

namespace TableTopFriend.Domain.Tests.AggregateSkill;

[TestFixture]
public class SkillTests
{
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();
    private readonly IDateTimeProvider _dateTimeProvider;

    public SkillTests()
    {
        var mockDate = DateTime.Parse("06/05/2023 00:00:00");
        _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(
            mockDate
        );
        _dateTimeProvider = _dateTimeProviderMock.Object;
    }

    [Test]
    public void Create_Skill_Should_Return_Valid_Skill()
    {
        const string name = "skill test";
        const string description = "skill test desc";
        const float cost = 10;
        UserId userId = UserId.CreateUnique();
        AudioEffectId audioEffectId = AudioEffectId.CreateUnique();
        StatusId statusId = StatusId.CreateUnique();
        DateTime createdAt = _dateTimeProvider.UtcNow;

        Name nameVo = Name.Create(name).Value;
        Description descriptionVo = Description.Create(description).Value;

        var skill = Skill.Create(
            userId,
            audioEffectId,
            statusId,
            name,
            description,
            cost,
            createdAt
        ).Value;

        var domainEvent = skill.GetDomainEvents().FirstOrDefault() as SkillCreatedDomainEvent;

        Assert.Multiple(() =>
        {
            Assert.That(skill.UserId, Is.EqualTo(userId));
            Assert.That(skill.AudioEffectId, Is.EqualTo(audioEffectId));
            Assert.That(skill.StatusId, Is.EqualTo(statusId));
            Assert.That(skill.Name, Is.EqualTo(nameVo));
            Assert.That(skill.Description, Is.EqualTo(descriptionVo));
            Assert.That(skill.Cost, Is.EqualTo(cost));
            Assert.That(skill.CreatedAt, Is.EqualTo(createdAt));
            Assert.That(domainEvent!.UserId, Is.EqualTo(userId));
            Assert.That(domainEvent!.AudioEffectId, Is.EqualTo(audioEffectId));
            Assert.That(domainEvent!.StatusId, Is.EqualTo(statusId));
            Assert.That(domainEvent!.Name, Is.EqualTo(nameVo));
            Assert.That(domainEvent!.Description, Is.EqualTo(descriptionVo));
            Assert.That(domainEvent!.Cost, Is.EqualTo(cost));
            Assert.That(domainEvent!.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Update_Skill_Should_Update_The_Skill_Instance()
    {
        const string name = "skill test";
        const string description = "skill test desc";
        const float cost = 10;
        UserId userId = UserId.CreateUnique();
        AudioEffectId audioEffectId = AudioEffectId.CreateUnique();
        StatusId statusId = StatusId.CreateUnique();
        DateTime createdAt = _dateTimeProvider.UtcNow;

        const string nameUpdated = "skill test updated";
        const string descriptionUpdated = "skill test desc updated";
        const float costUpdated = 10;
        UserId userIdUpdated = UserId.CreateUnique();
        AudioEffectId audioEffectIdUpdated = AudioEffectId.CreateUnique();
        StatusId statusIdUpdated = StatusId.CreateUnique();
        DateTime updatedAt = _dateTimeProvider.UtcNow;

        Name nameUpdatedVo = Name.Create(nameUpdated).Value;
        Description descriptionUpdatedVo = Description.Create(descriptionUpdated).Value;

        var skill = Skill.Create(
            userId,
            audioEffectId,
            statusId,
            name,
            description,
            cost,
            createdAt
        ).Value;

        skill.ClearDomainEvents();
        skill.Update(
            audioEffectIdUpdated,
            statusIdUpdated,
            nameUpdated,
            descriptionUpdated,
            costUpdated,
            updatedAt
        );

        var domainEvent = skill.GetDomainEvents().FirstOrDefault() as SkillChangedDomainEvent;

        Assert.Multiple(() =>
        {
            Assert.That(skill.AudioEffectId, Is.EqualTo(audioEffectIdUpdated));
            Assert.That(skill.StatusId, Is.EqualTo(statusIdUpdated));
            Assert.That(skill.Name, Is.EqualTo(nameUpdatedVo));
            Assert.That(skill.Description, Is.EqualTo(descriptionUpdatedVo));
            Assert.That(skill.Cost, Is.EqualTo(costUpdated));
            Assert.That(skill.UpdatedAt, Is.EqualTo(updatedAt));
            Assert.That(domainEvent!.UserId, Is.EqualTo(userId));
            Assert.That(domainEvent!.AudioEffectId, Is.EqualTo(audioEffectIdUpdated));
            Assert.That(domainEvent!.StatusId, Is.EqualTo(statusIdUpdated));
            Assert.That(domainEvent!.Name, Is.EqualTo(nameUpdatedVo));
            Assert.That(domainEvent!.Description, Is.EqualTo(descriptionUpdatedVo));
            Assert.That(domainEvent!.Cost, Is.EqualTo(costUpdated));
            Assert.That(domainEvent!.UpdatedAt, Is.EqualTo(updatedAt));
        });
    }

    [Test]
    public void Mark_To_Delete_Should_Return_Deleted_Skill_Domain_Event_Valid()
    {
        const string name = "skill test";
        const string description = "skill test desc";
        const float cost = 10;
        UserId userId = UserId.CreateUnique();
        AudioEffectId audioEffectId = AudioEffectId.CreateUnique();
        StatusId statusId = StatusId.CreateUnique();
        DateTime createdAt = _dateTimeProvider.UtcNow;
        DateTime deletedAt = _dateTimeProvider.UtcNow;

        var skill = Skill.Create(
            userId,
            audioEffectId,
            statusId,
            name,
            description,
            cost,
            createdAt
        ).Value;

        skill.ClearDomainEvents();
        skill.MarkToDelete(deletedAt);
        var domainEvent = skill.GetDomainEvents().FirstOrDefault() as SkillDeletedDomainEvent;

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent!.UserId, Is.EqualTo(userId));
            Assert.That(domainEvent!.SkillId, Is.EqualTo(SkillId.Create(skill.Id.Value)));
            Assert.That(domainEvent!.DeletedAt, Is.EqualTo(deletedAt));
        });
    }
}
