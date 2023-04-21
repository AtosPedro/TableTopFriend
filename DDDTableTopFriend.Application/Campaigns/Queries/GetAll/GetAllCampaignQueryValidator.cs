using DDDTableTopFriend.Application.Campaigns.GetAll.Queries;
using FluentValidation;

namespace DDDTableTopFriend.Application.Campaigns.Queries.GetAll;

public class GetAllCampaignQueryValidator : AbstractValidator<GetAllCampaignQuery>
{
    public GetAllCampaignQueryValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .NotNull();
    }
}
