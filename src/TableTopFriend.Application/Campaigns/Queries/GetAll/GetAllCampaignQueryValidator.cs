using TableTopFriend.Application.Campaigns.GetAll.Queries;
using FluentValidation;

namespace TableTopFriend.Application.Campaigns.Queries.GetAll;

public class GetAllCampaignQueryValidator : AbstractValidator<GetAllCampaignQuery>
{
    public GetAllCampaignQueryValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .NotNull();
    }
}
