using DDDTableTopFriend.Application.Characters.Common;
using FluentValidation;

namespace DDDTableTopFriend.Application.Characters.Commands.Create;

public class CharacterSheetDtoValidator : AbstractValidator<CharacterSheetDto>
{
    public CharacterSheetDtoValidator()
    {
        RuleFor(csh => csh.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(csh => csh.Description)
            .NotEmpty()
            .NotNull();
    }
}
