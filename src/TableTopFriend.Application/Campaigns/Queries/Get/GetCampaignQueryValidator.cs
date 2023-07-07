using TableTopFriend.Application.Campaigns.Get.Queries;
using FluentValidation;

namespace TableTopFriend.Application.Campaigns.Queries.Get;

public class GetCampaignQueryValidator : AbstractValidator<GetCampaignQuery>
{
    public GetCampaignQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .NotNull();
    }
}
