using FluentValidation;

namespace DDDTableTopFriend.Application.Skills.Commands.Create;

public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillCommandValidator()
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
