using FluentValidation;

namespace DDDTableTopFriend.Application.Characters.Queries.GetAll;

public class GetAllCharactersQueryValidator : AbstractValidator<GetAllCharactersQuery>
{
    public GetAllCharactersQueryValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .NotNull();
    }
}
