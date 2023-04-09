using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.AggregateUser;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        return (await Search(w => w.Email == email, cancellationToken)).FirstOrDefault();
    }
}
