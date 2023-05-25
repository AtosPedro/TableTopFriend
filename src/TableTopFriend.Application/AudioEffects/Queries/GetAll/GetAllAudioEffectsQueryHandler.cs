using TableTopFriend.Application.AudioEffects.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.AudioEffects.Queries.GetAll;

public class GetAllAudioEffectsQueryHandler : IRequestHandler<GetAllAudioEffectsQuery, ErrorOr<IReadOnlyList<AudioEffectResult>>>
{
    private readonly IAudioEffectRepository _audioEffectRepository;
    private readonly ICachingService _cachingService;
    public GetAllAudioEffectsQueryHandler(
        IAudioEffectRepository audioRepository,
        ICachingService cachingService)
    {
        _audioEffectRepository = audioRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<IReadOnlyList<AudioEffectResult>>> Handle(
        GetAllAudioEffectsQuery request,
        CancellationToken cancellationToken)
    {
        var cachedAudioEffects = await _cachingService.GetManyCacheValueAsync<AudioEffectResult>(w => w.UserId == request.UserId);
        if (cachedAudioEffects.Any())
            return cachedAudioEffects;

        var audioEffects = await _audioEffectRepository.GetAll(
            UserId.Create(request.UserId),
            cancellationToken);

        var audioEffectResults = new List<AudioEffectResult>();
        foreach (var audioEffect in audioEffects)
        {
            var result = audioEffect.Adapt<AudioEffectResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            audioEffectResults.Add(result);
        }

        return audioEffectResults;
    }
}
