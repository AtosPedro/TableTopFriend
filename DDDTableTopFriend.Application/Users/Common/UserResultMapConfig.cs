using DDDTableTopFriend.Domain.AggregateUser;
using Mapster;

namespace DDDTableTopFriend.Application.Users.Common;

public class UserResultMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResult>()
            .Map(dest => dest.Id, src => src.GetId().Value);
    }
}
