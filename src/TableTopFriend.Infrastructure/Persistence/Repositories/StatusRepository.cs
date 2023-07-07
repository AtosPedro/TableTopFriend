using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Domain.AggregateStatus;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Infrastructure.Persistence.Interfaces;

namespace TableTopFriend.Infrastructure.Persistence.Repositories;

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
