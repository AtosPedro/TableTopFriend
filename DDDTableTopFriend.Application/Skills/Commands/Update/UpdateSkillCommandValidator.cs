using FluentValidation;

namespace DDDTableTopFriend.Application.Skills.Commands.Update;

public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
{
    public UpdateSkillCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.StatusId)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Cost)
            .NotNull();
    }
}
