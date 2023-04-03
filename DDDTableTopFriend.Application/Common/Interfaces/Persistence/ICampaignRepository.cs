using DDDTableTopFriend.Domain.Entities;

namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface ICampaignRepository
{
    void Add(Campaign user);
    Campaign? GetById(Guid id);
    Campaign? GetByName(string name);
}
