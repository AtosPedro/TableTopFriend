using FluentValidation;

namespace DDDTableTopFriend.Application.Skills.Commands.Delete;

public class DeleteSkillCommandValidator : AbstractValidator<DeleteSkillCommand>
{
    public DeleteSkillCommandValidator()
    {
        RuleFor(c => c.SkillId)
            .NotEmpty()
            .NotNull();
    }
}
