using FluentValidation;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Update;

public class UpdateCampaignCommandValidator : AbstractValidator<UpdateCampaignCommand>
{
    public UpdateCampaignCommandValidator()
    {
    }
}
