using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _applicationDbContext;
    public UnitOfWork(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    private async Task Commit(CancellationToken cancellationToken = default)
    {
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> Execute<T>(Func<CancellationToken,Task<T>> getData, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await getData(cancellationToken);
            await Commit(cancellationToken);
            return result;
        }
        catch
        {
            await Rollback();
            throw;
        }
    }

    private async Task Rollback()
    {
        await Task.FromResult(true);
    }
}
