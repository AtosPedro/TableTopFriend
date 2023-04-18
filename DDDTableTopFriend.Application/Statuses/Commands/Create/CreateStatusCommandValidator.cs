using FluentValidation;

namespace DDDTableTopFriend.Application.Statuses.Commands.Create;

public class CreateStatusCommandValidator : AbstractValidator<CreateStatusCommand>
{
    public CreateStatusCommandValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(s => s.Description)
            .NotEmpty()
            .NotNull();

        RuleFor(s => s.Quantity)
            .NotNull();
    }
}
