using System.Linq.Expressions;
using TableTopFriend.Domain.AggregateUser;
using TableTopFriend.Domain.AggregateUser.ValueObjects;

namespace TableTopFriend.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task<User> Add(User user, CancellationToken cancellationToken);
    Task<IEnumerable<User>> SearchAsNoTracking(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
    Task<User?> GetById(UserId id, CancellationToken cancellationToken);
    Task<User> Update(User user);
    Task<User> Remove(User user);
}
