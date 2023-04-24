using System.Linq.Expressions;
using DDDTableTopFriend.Domain.AggregateSkill;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface ISkillRepository
{
    Task<IEnumerable<Skill>> SearchAsNoTracking(Expression<Func<Skill, bool>> predicate, CancellationToken cancellationToken);
    Task<Skill?> GetById(SkillId id, CancellationToken cancellationToken);
    Task<IEnumerable<Skill>> GetAll(UserId userId, CancellationToken cancellationToken);
    Task<Skill> Add(Skill skill, CancellationToken cancellationToken);
    Task<Skill> Update(Skill skill);
    Task<Skill> Remove(Skill skill);
    Task<Skill?> GetByName(UserId userId,string name, CancellationToken cancellationToken);
}
