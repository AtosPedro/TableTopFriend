using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using NUnit.Framework;

namespace DDDTableTopFriend.Domain.Tests.AggregateSkill.ValueObjects;

[TestFixture]
public class SkillIdTests
{
    [Test]
    public void Create_Unique_Should_Return_SkillId_Valid()
    {
        SkillId skillId = SkillId.CreateUnique();
        Assert.Multiple(() =>
        {
            Assert.That(skillId, Is.Not.Null);
            Assert.That(skillId.Value, Is.Not.EqualTo(default(Guid)));
        });
    }

    [Test]
    public void Create_Should_Return_SkillId_With_The_Passed_Value()
    {
        var id = Guid.NewGuid();
        SkillId skillId = SkillId.Create(id);
        Assert.Multiple(() =>
        {
            Assert.That(skillId, Is.Not.Null);
            Assert.That(skillId.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(skillId.Value, Is.EqualTo(id));
        });
    }
}

