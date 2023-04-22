using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.AggregateAudioEffect;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence.Repositories;

public class AudioEffectRepository : Repository<AudioEffect, AudioEffectId, Guid>, IAudioEffectRepository
{
    public AudioEffectRepository(
        IApplicationDbContext dbContext,
        IUnitOfWork unitOfWork) : base(dbContext, unitOfWork) { }

    public async Task<IEnumerable<AudioEffect>> GetAll(
        UserId userId,
        CancellationToken cancellationToken)
    {
        return await Search(w => w.UserId == userId, cancellationToken);
    }

    public async Task<AudioEffect?> GetById(
        AudioEffectId id,
        CancellationToken cancellationToken)
    {
        return await base.GetById(id, cancellationToken);
    }
}
