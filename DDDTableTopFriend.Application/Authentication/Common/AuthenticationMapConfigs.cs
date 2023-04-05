using DDDTableTopFriend.Domain.Entities;
using Mapster;

namespace DDDTableTopFriend.Application.Authentication.Common;

public class AuthenticationMapConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(User user, string token), AuthenticationResult>()
            .Map(dest => dest.Id, src => src.user.Id.Value)
            .Map(dest => dest.Email, src => src.user.Email)
            .Map(dest => dest.FirstName, src => src.user.FirstName)
            .Map(dest => dest.LastName, src => src.user.LastName)
            .Map(dest => dest.Token, src => src.token);
    }
}
