using Microsoft.EntityFrameworkCore;

namespace DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

public interface IApplicationDbContext
{
    DbSet<T> Set<T>() where T : class;
}
