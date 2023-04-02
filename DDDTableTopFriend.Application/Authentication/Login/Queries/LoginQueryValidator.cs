using FluentValidation;

namespace DDDTableTopFriend.Application.Authentication.Login.Queries;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();
    }
}
