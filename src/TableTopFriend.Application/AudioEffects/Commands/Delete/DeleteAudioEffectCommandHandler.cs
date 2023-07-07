using TableTopFriend.Application.AudioEffects.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.AudioEffects.Commands.Delete;

public class DeleteAudioEffectCommandHandler : IRequestHandler<DeleteAudioEffectCommand, ErrorOr<bool>>
{
    private readonly IAudioEffectRepository _audioEffectRepository;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAudioEffectCommandHandler(
        IAudioEffectRepository audioEffectRepository,
        ICachingService cachingService,
        IUnitOfWork unitOfWork)
    {
        _audioEffectRepository = audioEffectRepository;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
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

        return await _unitOfWork.Execute(async _ =>
        {
            await _audioEffectRepository.Remove(audioEffect);
            await _cachingService.RemoveCacheValueAsync<AudioEffectResult>(audioEffect.GetId().Value.ToString());
            return audioEffect is not null;
        },
        cancellationToken);
    }
}
