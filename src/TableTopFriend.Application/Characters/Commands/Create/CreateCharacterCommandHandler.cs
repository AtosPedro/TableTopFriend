using TableTopFriend.Application.Characters.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter;
using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Enums;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Characters.Commands.Create;

public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, ErrorOr<CharacterResult>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IAudioEffectRepository _audioEffectRepository;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateCharacterCommandHandler(
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

        if (character.IsError)
            return character.Errors;

        return await _unitOfWork.Execute(async cancellationToken =>
        {
            await _characterRepository.Add(character.Value, cancellationToken);
            var result = character.Adapt<CharacterResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        cancellationToken);
    }
}
