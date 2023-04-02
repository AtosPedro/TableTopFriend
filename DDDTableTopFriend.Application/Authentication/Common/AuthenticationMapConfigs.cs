using DDDTableTopFriend.Domain.Entities;
using Mapster;

namespace DDDTableTopFriend.Application.Authentication.Common;

public class AuthenticationMapConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(User user, string token), AuthenticationResult>()
            .Map(dest => dest.Token, src => src.token)
            .Map(dest => dest, src => src.user)
            .MapToConstructor(true);
    }
}
