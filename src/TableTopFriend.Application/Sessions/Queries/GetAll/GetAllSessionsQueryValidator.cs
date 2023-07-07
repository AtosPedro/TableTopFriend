using FluentValidation;

namespace TableTopFriend.Application.Sessions.Queries.GetAll;

public class GetAllSessionsQueryValidator : AbstractValidator<GetAllSessionsQuery>
{
    public GetAllSessionsQueryValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.CampaignId)
            .NotEmpty()
            .NotNull();
    }
}
