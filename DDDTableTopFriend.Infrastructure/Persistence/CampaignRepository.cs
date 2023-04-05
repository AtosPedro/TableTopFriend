using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.Campaign;

namespace DDDTableTopFriend.Infrastructure.Persistence;

public class CampaignRepository : ICampaignRepository
{
    public static List<Campaign> campaigns = new();

    public void Add(Campaign user)
    {
        campaigns.Add(user);
    }

    public IEnumerable<Campaign> GetAll()
    {
        return campaigns;
    }

    public Campaign? GetById(Guid id)
    {
       return campaigns.SingleOrDefault(u => u.Id.Value == id);
    }

    public Campaign? GetByName(string name)
    {
        return campaigns.SingleOrDefault(u => u.Name == name);
    }

    public void Update(Campaign campaign)
    {
        var c = GetById(campaign.Id.Value);
        if (c  is not null)
            campaigns.Remove(c);

        campaigns.Add(campaign);
    }
}
