using TableTopFriend.Application.Characters.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Characters.Queries.Get;

public class GetCharacterQueryHandler : IRequestHandler<GetCharacterQuery, ErrorOr<CharacterResult>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ICachingService _cachingService;

    public GetCharacterQueryHandler(
        ICharacterRepository characterRepository,
        ICachingService cachingService)
    {
        _characterRepository = characterRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<CharacterResult>> Handle(
        GetCharacterQuery request,
        CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetById(
            CharacterId.Create(request.CharacterId),
            cancellationToken);

        if (character is null)
            return Errors.Character.NotRegistered;

        var result = character.Adapt<CharacterResult>();
        await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
        return result;
    }
}
