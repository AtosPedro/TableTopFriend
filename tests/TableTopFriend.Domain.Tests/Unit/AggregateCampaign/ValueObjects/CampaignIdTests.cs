using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using NUnit.Framework;

namespace TableTopFriend.Domain.Tests.AggregateCampaign.ValueObjects;

[TestFixture]
public class CampaignIdTests
{
    [Test]
    [Author("Atos Pedro")]
    public void Create_Unique_Should_Return_CampaignId_Valid()
    {
        CampaignId campaignId = CampaignId.CreateUnique();
        Assert.Multiple(() =>
        {
            Assert.That(campaignId, Is.Not.Null);
            Assert.That(campaignId.Value, Is.Not.EqualTo(default(Guid)));
        });
    }

    [Test]
    [Author("Atos Pedro")]
    public void Create_Should_Return_UserId_With_The_Passed_Value()
    {
        var id = Guid.NewGuid();
        CampaignId campaignId = CampaignId.Create(id);
        Assert.Multiple(() =>
        {
            Assert.That(campaignId, Is.Not.Null);
            Assert.That(campaignId.Value, Is.Not.EqualTo(default(Guid)));
            Assert.That(campaignId.Value, Is.EqualTo(id));
        });
    }
}
