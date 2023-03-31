using DDDTableTopFriend.Domain.Entities;

namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}
