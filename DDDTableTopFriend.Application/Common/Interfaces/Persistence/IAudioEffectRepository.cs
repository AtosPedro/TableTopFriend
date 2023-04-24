using System.Linq.Expressions;
using DDDTableTopFriend.Domain.AggregateAudioEffect;
using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface IAudioEffectRepository
{
    Task<IEnumerable<AudioEffect>> SearchAsNoTracking(Expression<Func<AudioEffect, bool>> predicate, CancellationToken cancellationToken);
    Task<AudioEffect?> GetById(AudioEffectId id, CancellationToken cancellationToken);
    Task<IEnumerable<AudioEffect>> GetAll(UserId userId, CancellationToken cancellationToken);
    Task<AudioEffect> Add(AudioEffect audioEffect, CancellationToken cancellationToken);
    Task<AudioEffect> Update(AudioEffect audioEffect);
    Task<AudioEffect> Remove(AudioEffect audioEffect);
}
