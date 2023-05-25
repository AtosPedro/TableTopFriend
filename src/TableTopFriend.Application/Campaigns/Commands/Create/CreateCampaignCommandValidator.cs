using FluentValidation;

namespace TableTopFriend.Application.Campaigns.Commands.Create;

public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
{
    public CreateCampaignCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull();
    }
}
