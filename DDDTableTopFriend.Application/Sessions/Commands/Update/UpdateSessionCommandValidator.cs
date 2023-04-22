using System.Data;
using FluentValidation;

namespace DDDTableTopFriend.Application.Sessions.Commands.Update;

public class UpdateSessionCommandValidator : AbstractValidator<UpdateSessionCommand>
{
    public UpdateSessionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.DateTime)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty();
    }
}
