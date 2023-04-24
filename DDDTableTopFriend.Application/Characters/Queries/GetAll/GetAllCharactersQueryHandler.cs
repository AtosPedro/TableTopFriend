using DDDTableTopFriend.Application.Characters.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Characters.Queries.GetAll;

public class GetAllCharactersQueryHandler : IRequestHandler<GetAllCharactersQuery, ErrorOr<IReadOnlyList<CharacterResult>>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ICachingService _cachingService;
    public GetAllCharactersQueryHandler(
        ICharacterRepository characterRepository,
        ICachingService cachingService)
    {
        _characterRepository = characterRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<IReadOnlyList<CharacterResult>>> Handle(
        GetAllCharactersQuery request,
        CancellationToken cancellationToken)
    {
        var cachedCharacters = await _cachingService.GetManyCacheValueAsync<CharacterResult>(w => w.UserId == request.UserId);
        if (cachedCharacters.Any())
            return cachedCharacters;

        var characters = await _characterRepository.GetAll(
            UserId.Create(request.UserId),
            cancellationToken);

        var characterResults = new List<CharacterResult>();
        foreach (var character in characters)
        {
            var result = character.Adapt<CharacterResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            characterResults.Add(result);
        }
        return characterResults;
    }
}
