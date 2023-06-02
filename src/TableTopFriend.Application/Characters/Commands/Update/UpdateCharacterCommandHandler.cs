using TableTopFriend.Application.Characters.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.ValueObjects;
using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.Common.Enums;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Characters.Commands.Update;

public class UpdateCharacterCommandHandler : IRequestHandler<UpdateCharacterCommand, ErrorOr<CharacterResult>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IAudioEffectRepository _audioEffectRepository;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateCharacterCommandHandler(
        ICharacterRepository characterRepository,
        ISkillRepository skillRepository,
        IStatusRepository statusRepository,
        IAudioEffectRepository audioEffectRepository,
        ICachingService cachingService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _characterRepository = characterRepository;
        _skillRepository = skillRepository;
        _statusRepository = statusRepository;
        _audioEffectRepository = audioEffectRepository;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<CharacterResult>> Handle(
        UpdateCharacterCommand request,
        CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetById(
            CharacterId.Create(request.Id),
            cancellationToken);

        if (character is null)
            return Errors.Character.NotRegistered;

        var audioEffectsIds = request.AudioEffectIds.ConvertAll(guid => AudioEffectId.Create(guid));
        var statusIds = request.CharacterSheet.StatusIds.ConvertAll(guid => StatusId.Create(guid));
        var skillIds = request.CharacterSheet.SkillIds.ConvertAll(guid => SkillId.Create(guid));

        if (audioEffectsIds.Any())
        {
            var existsAllAudioEffects = (await _audioEffectRepository.SearchAsNoTracking(
                au => audioEffectsIds.Contains(au.Id),
                cancellationToken)).Count() == audioEffectsIds.Count;

            if (!existsAllAudioEffects)
                return Errors.AudioEffect.NotRegistered;
        }

        if (statusIds.Any())
        {
            var existsAllStatuses = (await _statusRepository.SearchAsNoTracking(
                au => statusIds.Contains(au.Id),
                cancellationToken)).Count() == statusIds.Count;

            if (!existsAllStatuses)
                return Errors.Status.NotRegistered;
        }

        if (skillIds.Any())
        {
            var existsAllSkills = (await _skillRepository.SearchAsNoTracking(
                    au => skillIds.Contains(au.Id),
                    cancellationToken)).Count() == skillIds.Count;

            if (!existsAllSkills)
                return Errors.Skill.NotRegistered;
        }

        character.Update(
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

        return await _unitOfWork.Execute(async _ =>
        {
            await _characterRepository.Update(character);
            var result = character.Adapt<CharacterResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        cancellationToken);
    }
}
