using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Domain.AggregateSkill;
using TableTopFriend.Domain.AggregateSkill.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.ValueObjects;
using TableTopFriend.Infrastructure.Persistence.Interfaces;

namespace TableTopFriend.Infrastructure.Persistence.Repositories;

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
