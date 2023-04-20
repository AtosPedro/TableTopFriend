using FluentValidation;

namespace DDDTableTopFriend.Application.Sessions.Commands.Update;

public class UpdateSessionCommandValidator : AbstractValidator<UpdateSessionCommand>
{
    public UpdateSessionCommandValidator()
    {
    }
}
