using TableTopFriend.Domain.AggregateUser;
using Mapster;

namespace TableTopFriend.Application.Authentication.Common;

public class AuthenticationMapConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(User user, string token), AuthenticationResult>()
            .Map(dest => dest.Id, src => src.user.GetId().Value)
            .Map(dest => dest.Email, src => src.user.Email.Value)
            .Map(dest => dest.FirstName, src => src.user.FirstName)
            .Map(dest => dest.LastName, src => src.user.LastName)
            .Map(dest => dest.Token, src => src.token);
    }
}
