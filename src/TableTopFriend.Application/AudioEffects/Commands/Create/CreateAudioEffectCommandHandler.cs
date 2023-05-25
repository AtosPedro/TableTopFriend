using TableTopFriend.Application.AudioEffects.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateAudioEffect;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.AudioEffects.Commands.Create;

public class CreateAudioEffectCommandHandler : IRequestHandler<CreateAudioEffectCommand, ErrorOr<AudioEffectResult>>
{
    private readonly IAudioEffectRepository _audioEffectRepository;
    private readonly ICachingService _cachingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    public CreateAudioEffectCommandHandler(
        IAudioEffectRepository audioEffectRepository,
        ICachingService cachingService,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _audioEffectRepository = audioEffectRepository;
        _cachingService = cachingService;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AudioEffectResult>> Handle(
        CreateAudioEffectCommand request,
        CancellationToken cancellationToken)
    {
        var audioEffectOrError = AudioEffect.Create(
            UserId.Create(request.UserId),
            request.Name,
            request.Description,
            request.AudioLink,
            request.AudioClip,
            _dateTimeProvider.UtcNow
        );

        if (audioEffectOrError.IsError)
            return audioEffectOrError.Errors;

        return await _unitOfWork.Execute(async _ =>
        {
            await _audioEffectRepository.Add(audioEffectOrError.Value, cancellationToken);
            var result = audioEffectOrError.Value.Adapt<AudioEffectResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        cancellationToken);
    }
}
