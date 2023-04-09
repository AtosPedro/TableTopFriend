using DDDTableTopFriend.Domain.AggregateUser;

namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
    Task<User> Add(User user, CancellationToken cancellationToken);
}
