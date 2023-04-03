using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.Entities;

namespace DDDTableTopFriend.Infrastructure.Persistence;

public class CampaignRepository : ICampaignRepository
{
    public static List<Campaign> campaigns = new();

    public void Add(Campaign user)
    {
        campaigns.Add(user);
    }

    public Campaign? GetById(Guid id)
    {
        return campaigns.SingleOrDefault(u => u.Id == id);
    }

    public Campaign? GetByName(string name)
    {
        return campaigns.SingleOrDefault(u => u.Name == name);
    }
}
