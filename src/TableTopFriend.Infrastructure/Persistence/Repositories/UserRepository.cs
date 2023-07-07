using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Domain.AggregateUser;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Infrastructure.Persistence.Interfaces;

namespace TableTopFriend.Infrastructure.Persistence.Repositories;

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
        return (await SearchAsNoTracking(w => w.Email.Value == email, cancellationToken)).FirstOrDefault();
    }
}
