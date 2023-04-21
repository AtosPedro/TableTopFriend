using DDDTableTopFriend.Domain.AggregateAudioEffect;
using Mapster;

namespace DDDTableTopFriend.Application.AudioEffects.Common;

public class AudioEffectResultMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AudioEffect, AudioEffectResult>()
            .Map(dest => dest.UserId, src => src.UserId.Value)
            .Map(dest => dest.Id, src => src.GetId().Value);
    }
}
