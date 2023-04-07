using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.Entities;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

namespace DDDTableTopFriend.Infrastructure.Persistence;

public class UserRepository : Repository<User>, IUserRepository
{
    public static List<User> users = new();

    public UserRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public void Add(User user)
    {
        users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return users.SingleOrDefault(u => u.Email == email);
    }
}
