using DDDTableTopFriend.Application.AudioEffects.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Delete;

public class DeleteAudioEffectCommandHandler : IRequestHandler<DeleteAudioEffectCommand, ErrorOr<bool>>
{
    private readonly IAudioEffectRepository _audioEffectRepository;
    private readonly ICachingService _cachingService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DeleteAudioEffectCommandHandler(
        IAudioEffectRepository audioEffectRepository,
        ICachingService cachingService,
        IDateTimeProvider dateTimeProvider)
    {
        _audioEffectRepository = audioEffectRepository;
        _cachingService = cachingService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<bool>> Handle(
        DeleteAudioEffectCommand request,
        CancellationToken cancellationToken)
    {
        var audioEffect = await _audioEffectRepository.GetById(
            AudioEffectId.Create(request.Id),
            cancellationToken);
        
        if (audioEffect is null)
            return Errors.AudioEffect.NotRegistered;

        await _audioEffectRepository.Remove(audioEffect);
        await _cachingService.RemoveCacheValueAsync<AudioEffectResult>(audioEffect.GetId().Value.ToString());
        return audioEffect is not null;
    }
}
