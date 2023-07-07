using FluentValidation;

namespace TableTopFriend.Application.Characters.Queries.Get;

public class GetCharacterQueryValidator : AbstractValidator<GetCharacterQuery>
{
    public GetCharacterQueryValidator()
    {
        RuleFor(c => c.CharacterId)
            .NotEmpty()
            .NotNull();
    }
}
