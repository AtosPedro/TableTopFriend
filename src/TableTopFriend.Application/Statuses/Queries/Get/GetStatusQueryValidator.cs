using FluentValidation;

namespace TableTopFriend.Application.Statuses.Queries.Get;

public class GetStatusQueryValidator : AbstractValidator<GetStatusQuery>
{
    public GetStatusQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .NotNull();
    }
}
