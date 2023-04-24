using FluentValidation;

namespace DDDTableTopFriend.Application.Characters.Queries.Get;

public class GetCharacterQueryValidator : AbstractValidator<GetCharacterQuery>
{
    public GetCharacterQueryValidator()
    {
        RuleFor(c => c.CharacterId)
            .NotEmpty()
            .NotNull();
    }
}
