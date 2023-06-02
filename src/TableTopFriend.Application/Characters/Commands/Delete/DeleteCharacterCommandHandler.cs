using System.Linq;
using TableTopFriend.Application.Campaigns.Common;
using TableTopFriend.Application.Characters.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Characters.Commands.Delete;

public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, ErrorOr<bool>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCharacterCommandHandler(
        ICharacterRepository characterRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService,
        IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(
        DeleteCharacterCommand request,
        CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetById(
            CharacterId.Create(request.CharacterId),
            cancellationToken);

        if (character is null)
            return Errors.Character.NotRegistered;

        character.MarkToDelete(_dateTimeProvider.UtcNow);

        return await _unitOfWork.Execute(async _ =>
        {
            await _characterRepository.Remove(character);
            await _cachingService.RemoveCacheValueAsync<CharacterResult>(request.CharacterId.ToString());
            return character is not null;
        },
        cancellationToken);
    }
}
