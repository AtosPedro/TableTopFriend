using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Infrastructure.Persistence.Interfaces;

namespace TableTopFriend.Infrastructure.Persistence.Repositories;

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
            throw;
        }
    }
}
