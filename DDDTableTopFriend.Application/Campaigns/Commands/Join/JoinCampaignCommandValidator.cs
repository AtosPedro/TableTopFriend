using FluentValidation;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Join;

public class JoinCampaignCommandValidator : AbstractValidator<JoinCampaignCommand>
{
    public JoinCampaignCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();
        
        RuleFor(x => x.CharacterId)
            .NotEmpty()
            .NotNull();
    }
}
