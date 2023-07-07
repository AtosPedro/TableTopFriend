using FluentValidation;

namespace TableTopFriend.Application.Statuses.Commands.Update;

public class UpdateStatusCommandValidator : AbstractValidator<UpdateStatusCommand>
{
    public UpdateStatusCommandValidator()
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
