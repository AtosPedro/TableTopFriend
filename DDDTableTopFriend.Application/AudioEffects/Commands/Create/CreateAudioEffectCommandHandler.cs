using DDDTableTopFriend.Application.AudioEffects.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateAudioEffect;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Commands.Create;

public class CreateAudioEffectCommandHandler : IRequestHandler<CreateAudioEffectCommand, ErrorOr<AudioEffectResult>>
{
    private readonly IAudioEffectRepository _audioEffectRepository;
    private readonly ICachingService _cachingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    public CreateAudioEffectCommandHandler(
        IAudioEffectRepository audioEffectRepository,
        ICachingService cachingService,
        IDateTimeProvider dateTimeProvider)
    {
        _audioEffectRepository = audioEffectRepository;
        _cachingService = cachingService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<AudioEffectResult>> Handle(
        CreateAudioEffectCommand request,
        CancellationToken cancellationToken)
    {
        var audioEffect = AudioEffect.Create(
            UserId.Create(request.UserId),
            request.Name,
            request.Description,
            request.AudioLink,
            request.AudioClip,
            _dateTimeProvider.UtcNow
        );

        await _audioEffectRepository.Add(audioEffect, cancellationToken);
        var result = audioEffect.Adapt<AudioEffectResult>();
        await _cachingService.SetCacheValueAsync<AudioEffectResult>(result.Id.ToString(),result);
        return result;
    }
}
