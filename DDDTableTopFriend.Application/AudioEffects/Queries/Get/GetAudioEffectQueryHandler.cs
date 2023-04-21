using DDDTableTopFriend.Application.AudioEffects.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.AudioEffects.Queries.Get;

public class GetAudioEffectQueryHandler : IRequestHandler<GetAudioEffectQuery, ErrorOr<AudioEffectResult>>
{
    private readonly IAudioEffectRepository _audioEffectRepository;
    private readonly ICachingService _cachingService;

    public GetAudioEffectQueryHandler(
        IAudioEffectRepository audioRepository,
        ICachingService cachingService)
    {
        _audioEffectRepository = audioRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<AudioEffectResult>> Handle(
        GetAudioEffectQuery request,
        CancellationToken cancellationToken)
    {
        var cachedAudioEffect = await _cachingService.GetCacheValueAsync<AudioEffectResult>(request.Id.ToString());
        if(cachedAudioEffect is not null)
            return cachedAudioEffect;

        var audioEffect = await _audioEffectRepository.GetById(
            AudioEffectId.Create(request.Id),
            cancellationToken);

        if(audioEffect is null)
            return Errors.Campaign.NotRegistered;

        var result = audioEffect.Adapt<AudioEffectResult>();
        await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
        return result;
    }
}
