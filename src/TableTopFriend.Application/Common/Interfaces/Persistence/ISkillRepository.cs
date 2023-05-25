using System.Linq.Expressions;
using TableTopFriend.Domain.AggregateSkill;
using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Application.Common.Interfaces.Persistence;

public interface ISkillRepository
{
    Task<IEnumerable<Skill>> SearchAsNoTracking(Expression<Func<Skill, bool>> predicate, CancellationToken cancellationToken);
    Task<Skill?> GetById(SkillId id, CancellationToken cancellationToken);
    Task<IEnumerable<Skill>> GetAll(UserId userId, CancellationToken cancellationToken);
    Task<Skill> Add(Skill skill, CancellationToken cancellationToken);
    Task<Skill> Update(Skill skill);
    Task<Skill> Remove(Skill skill);
    Task<Skill?> GetByName(UserId userId, Name name, CancellationToken cancellationToken);
}
