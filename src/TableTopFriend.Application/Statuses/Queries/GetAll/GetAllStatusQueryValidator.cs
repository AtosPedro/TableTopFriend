using FluentValidation;

namespace TableTopFriend.Application.Statuses.Queries.GetAll;

public class GetAllStatusQueryValidator : AbstractValidator<GetAllStatusQuery>
{
    public GetAllStatusQueryValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull()
            .NotEmpty();
    }
}
