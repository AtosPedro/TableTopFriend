using FluentValidation;

namespace TableTopFriend.Application.Skills.Queries.GetAll;

public class GetAllSkillsQueryValidator : AbstractValidator<GetAllSkillsQuery>
{
    public GetAllSkillsQueryValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .NotNull();
    }
}
