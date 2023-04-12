using FluentValidation;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Delete;

public class DeleteCampaignCommandValidator : AbstractValidator<DeleteCampaignCommand>
{
    public DeleteCampaignCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();
    }
}
