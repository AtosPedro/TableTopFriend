using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _applicationDbContext;

    public UnitOfWork(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Rollback()
    {
        await Task.FromResult(true);
    }
}
