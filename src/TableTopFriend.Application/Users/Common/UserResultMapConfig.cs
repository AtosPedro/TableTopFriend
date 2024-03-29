using TableTopFriend.Domain.AggregateUser;
using Mapster;

namespace TableTopFriend.Application.Users.Common;

public class UserResultMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResult>()
            .Map(dest => dest.Id, src => src.GetId().Value);
    }
}
