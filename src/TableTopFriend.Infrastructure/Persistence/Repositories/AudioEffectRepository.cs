using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Domain.AggregateAudioEffect;
using TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Infrastructure.Persistence.Interfaces;

namespace TableTopFriend.Infrastructure.Persistence.Repositories;

public class AudioEffectRepository : Repository<AudioEffect, AudioEffectId, Guid>, IAudioEffectRepository
{
    public AudioEffectRepository(
        IApplicationDbContext dbContext,
        IUnitOfWork unitOfWork) : base(dbContext, unitOfWork) { }

    public async Task<IEnumerable<AudioEffect>> GetAll(
        UserId userId,
        CancellationToken cancellationToken)
    {
        return await SearchAsNoTracking(w => w.UserId == userId, cancellationToken);
    }

    public async Task<AudioEffect?> GetById(
        AudioEffectId id,
        CancellationToken cancellationToken)
    {
        return await base.GetById(id, cancellationToken);
    }
}
