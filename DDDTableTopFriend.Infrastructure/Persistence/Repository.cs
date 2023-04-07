using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence;

public abstract class Repository<T>
{
    protected readonly IApplicationDbContext _dbContext;
    protected Repository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
