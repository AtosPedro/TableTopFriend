using System.Linq;
using DDDTableTopFriend.Application.Characters.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Characters.Commands.Delete;

public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, ErrorOr<bool>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ICampaignRepository _campaignRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCharacterCommandHandler(
        ICharacterRepository characterRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService,
        IUnitOfWork unitOfWork,
        ICampaignRepository campaignRepository)
    {
        _characterRepository = characterRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
        _campaignRepository = campaignRepository;
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

        var campaigns = await _campaignRepository.Search(
            camp => camp.CharacterIds.Contains(CharacterId.Create(request.CharacterId)),
            cancellationToken);

        foreach (var campaign in campaigns)
        {
            campaign.RemoveCharacterId(
                CharacterId.Create(request.CharacterId),
                _dateTimeProvider.UtcNow);
        }

        character.MarkToDelete(_dateTimeProvider.UtcNow);
        return await _unitOfWork.Execute(async _ =>
        {
            foreach (var campaign in campaigns)
                await _campaignRepository.Update(campaign);

            await _characterRepository.Remove(character);
            await _cachingService.RemoveCacheValueAsync<CharacterResult>(request.CharacterId.ToString());
            return character is not null;
        },
        character.DomainEvents,
        cancellationToken);
    }
}
