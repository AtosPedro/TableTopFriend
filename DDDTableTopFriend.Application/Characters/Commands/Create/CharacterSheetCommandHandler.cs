using DDDTableTopFriend.Application.Characters.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Characters.Commands.Create;

public class CharacterSheetCommandHandler : IRequestHandler<CreateCharacterCommand, ErrorOr<CharacterResult>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IAudioEffectRepository _audioEffectRepository;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CharacterSheetCommandHandler(
        ICharacterRepository characterRepository,
        ISkillRepository skillRepository,
        IStatusRepository statusRepository,
        ICachingService cachingService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IAudioEffectRepository audioEffectRepository)
    {
        _characterRepository = characterRepository;
        _skillRepository = skillRepository;
        _statusRepository = statusRepository;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _audioEffectRepository = audioEffectRepository;
    }

    public async Task<ErrorOr<CharacterResult>> Handle(
        CreateCharacterCommand request,
        CancellationToken cancellationToken)
    {
        var audioEffectsIds = request.AudioEffectIds.ConvertAll(guid => AudioEffectId.Create(guid));
        var statusIds = request.CharacterSheet.StatusIds.ConvertAll(guid => StatusId.Create(guid));
        var skillIds = request.CharacterSheet.SkillIds.ConvertAll(guid => SkillId.Create(guid));

        var existsAllAudioEffects = (await _audioEffectRepository.SearchAsNoTracking(
            au => audioEffectsIds.Contains(au.Id),
             cancellationToken)).Count() == audioEffectsIds.Count;

        if (!existsAllAudioEffects)
            return Errors.AudioEffect.NotRegistered;

        var existsAllStatuses = (await _statusRepository.SearchAsNoTracking(
            au => statusIds.Contains(au.Id),
             cancellationToken)).Count() == statusIds.Count;

        if (!existsAllStatuses)
            return Errors.Status.NotRegistered;

        var existsAllSkills = (await _skillRepository.SearchAsNoTracking(
                au => skillIds.Contains(au.Id),
                 cancellationToken)).Count() == skillIds.Count;

        if (!existsAllSkills)
            return Errors.Skill.NotRegistered;

        var character = Character.Create(
            UserId.Create(request.UserId),
            request.Name,
            request.Description,
            (CharacterType)request.Type,
            audioEffectsIds,
            request.CharacterSheet.Name,
            request.CharacterSheet.Description,
            statusIds,
            skillIds,
            _dateTimeProvider.UtcNow
        );

        return await _unitOfWork.Execute(async cancellationToken =>
        {
            await _characterRepository.Add(character, cancellationToken);
            var result = character.Adapt<CharacterResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        character.DomainEvents,
        cancellationToken);
    }
}
