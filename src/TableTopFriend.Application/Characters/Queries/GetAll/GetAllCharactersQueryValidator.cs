using FluentValidation;

namespace TableTopFriend.Application.Characters.Queries.GetAll;

public class GetAllCharactersQueryValidator : AbstractValidator<GetAllCharactersQuery>
{
    public GetAllCharactersQueryValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .NotNull();
    }
}
