using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.AggregateUser;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User, UserId, Guid> , IUserRepository
{
    public UserRepository(
        IApplicationDbContext dbContext,
        IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    public async Task<User?> GetById(
        UserId id,
        CancellationToken cancellationToken)
    {
       return await base.GetById(id, cancellationToken);
    }

    public async Task<User?> GetUserByEmail(
        string email,
        CancellationToken cancellationToken)
    {
        return (await SearchAsNoTracking(w => w.Email == email, cancellationToken)).FirstOrDefault();
    }
}
