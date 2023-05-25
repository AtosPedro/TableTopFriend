using FluentValidation;

namespace TableTopFriend.Application.Sessions.Queries.Get;

public class GetSessionQueryValidator : AbstractValidator<GetSessionQuery>
{
    public GetSessionQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .NotNull();
    }
}
