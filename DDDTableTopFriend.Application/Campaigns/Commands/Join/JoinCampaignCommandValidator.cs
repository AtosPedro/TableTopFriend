using FluentValidation;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Join;

public class JoinCampaignCommandValidator : AbstractValidator<JoinCampaignCommand>
{
    public JoinCampaignCommandValidator()
    {
    }
}
