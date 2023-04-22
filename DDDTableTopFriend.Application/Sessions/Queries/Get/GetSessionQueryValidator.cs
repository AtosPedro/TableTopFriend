using FluentValidation;

namespace DDDTableTopFriend.Application.Sessions.Queries.Get;

public class GetSessionQueryValidator : AbstractValidator<GetSessionQuery>
{
    public GetSessionQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .NotNull();
    }
}
