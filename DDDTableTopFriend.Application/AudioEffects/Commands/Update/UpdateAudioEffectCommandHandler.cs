using DDDTableTopFriend.Application.AudioEffects.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Update;

public class UpdateAudioEffectCommandHandler : IRequestHandler<UpdateAudioEffectCommand, ErrorOr<AudioEffectResult>>
{
    private readonly IAudioEffectRepository _audioEffectRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;

    public UpdateAudioEffectCommandHandler(
        IAudioEffectRepository audioEffectRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService)
    {
        _audioEffectRepository = audioEffectRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<AudioEffectResult>> Handle(
        UpdateAudioEffectCommand request,
        CancellationToken cancellationToken)
    {
        var audioEffect = await _audioEffectRepository.GetById(
            AudioEffectId.Create(request.Id),
            cancellationToken);

        if (audioEffect is null)
            return Errors.AudioEffect.NotRegistered;

        audioEffect.Update(
            request.Name,
            request.Description,
            request.AudioLink,
            request.AudioClip,
            _dateTimeProvider.UtcNow
        );

        await _audioEffectRepository.Update(audioEffect);
        var result = audioEffect.Adapt<AudioEffectResult>();
        await _cachingService.SetCacheValueAsync<AudioEffectResult>(result.Id.ToString(), result);
        return result;
    }
}
