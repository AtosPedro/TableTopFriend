using FluentValidation;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Create;

public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
{
    public CreateCampaignCommandValidator()
    {
    }
}
