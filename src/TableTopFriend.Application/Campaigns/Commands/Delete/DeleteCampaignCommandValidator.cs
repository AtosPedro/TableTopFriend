using FluentValidation;

namespace TableTopFriend.Application.Campaigns.Commands.Delete;

public class DeleteCampaignCommandValidator : AbstractValidator<DeleteCampaignCommand>
{
    public DeleteCampaignCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();
    }
}
