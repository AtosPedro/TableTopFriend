using FluentValidation;

namespace TableTopFriend.Application.Skills.Queries.Get;

public class GetSkillQueryValidator : AbstractValidator<GetSkillQuery>
{
    public GetSkillQueryValidator()
    {
        RuleFor(c => c.SkillId)
            .NotNull()
            .NotEmpty();
    }
}
