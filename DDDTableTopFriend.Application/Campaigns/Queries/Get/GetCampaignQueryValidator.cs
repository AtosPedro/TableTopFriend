using DDDTableTopFriend.Application.Campaigns.Get.Queries;
using FluentValidation;

namespace DDDTableTopFriend.Application.Campaigns.Queries.Get;

public class GetCampaignQueryValidator : AbstractValidator<GetCampaignQuery>
{
    public GetCampaignQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .NotNull();
    }
}
