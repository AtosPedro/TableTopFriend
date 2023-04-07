using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;

namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface ICampaignRepository
{
    void Add(Domain.Campaign.Campaign campaign);
    void Update(Domain.Campaign.Campaign campaign);
    Domain.Campaign.Campaign? GetByName(string name);
    Domain.Campaign.Campaign? GetById(Guid id);
    IEnumerable<Domain.Campaign.Campaign> GetAll();
    void Remove(Guid id);
}
