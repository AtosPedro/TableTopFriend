using DDDTableTopFriend.Domain.AggregateSkill;
using Mapster;

namespace DDDTableTopFriend.Application.Skills.Common;

public class SkillResultMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Skill, SkillResult>()
            .Map(dest => dest.Id, c => c.GetId().Value)
            .Map(dest => dest.AudioEffectId, c => c.AudioEffectId.Value)
            .Map(dest => dest.UserId, c => c.UserId.Value)
            .Map(dest => dest.StatusId, c => c.StatusId.Value);
    }
}
