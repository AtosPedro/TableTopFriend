using DDDTableTopFriend.Domain.AggregateStatus;
using Mapster;

namespace DDDTableTopFriend.Application.Statuses.Common;

public class StatusResultMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Status, StatusResult>()
            .Map(dest => dest.Id, src => src.GetId().Value)
            .Map(dest => dest.UserId, src => src.UserId.Value);
    }
}