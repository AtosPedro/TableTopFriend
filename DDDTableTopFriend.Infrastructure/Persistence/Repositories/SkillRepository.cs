using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.AggregateSkill;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.ValueObjects;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence.Repositories;

public class SkillRepository : Repository<Skill, SkillId, Guid>, ISkillRepository
{
    public SkillRepository(
        IApplicationDbContext dbContext,
        IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<IEnumerable<Skill>> GetAll(
        UserId userId,
        CancellationToken cancellationToken)
    {
        return await base.SearchAsNoTracking(w => w.UserId == userId, cancellationToken);
    }

    public async Task<Skill?> GetById(
        SkillId id,
        CancellationToken cancellationToken)
    {
        return await base.GetById(id, cancellationToken);
    }

    public async Task<Skill?> GetByName(UserId userId, Name name, CancellationToken cancellationToken)
    {
        return (await base.SearchAsNoTracking(w => w.Name.Value == name.Value && w.UserId == userId, cancellationToken)).FirstOrDefault();
    }
}
