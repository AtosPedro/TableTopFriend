using FluentValidation;

namespace TableTopFriend.Application.Statuses.Commands.Delete;

public class DeleteStatusCommandValidator : AbstractValidator<DeleteStatusCommand>
{
    public DeleteStatusCommandValidator()
    {
        RuleFor(c => c.StatusId)
            .NotEmpty()
            .NotNull();
    }
}
