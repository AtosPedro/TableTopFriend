using System.Security.Cryptography.X509Certificates;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using FluentValidation;

namespace DDDTableTopFriend.Application.Authentication.Register.Commands;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .NotNull()
            .Length(3, 20);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .NotNull()
            .Length(3, 20);
    }
}
