using DDDTableTopFriend.Domain.Common.ValueObjects;
using Mapster;

namespace DDDTableTopFriend.Application.Common.Mappings;

public class AggregateRootId : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Guid, AggregateRootId<Guid>>()
            .Map( dest => dest.Value, src => src);
    }
}
