using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.AggregateStatus;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence.Repositories;

public class StatusRepository : Repository<Status, StatusId, Guid>, IStatusRepository
{
    public StatusRepository(
        IApplicationDbContext dbContext,
        IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<IEnumerable<Status>> GetAll(
        UserId userId,
        CancellationToken cancellationToken)
    {
        return await base.SearchAsNoTracking(w=> w.UserId == userId, cancellationToken);
    }

    public async Task<Status?> GetById(
        StatusId id,
        CancellationToken cancellationToken)
    {
        return await base.GetById(id, cancellationToken);
    }
}
