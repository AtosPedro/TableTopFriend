using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateCharacter.Entities;
using Mapster;

namespace DDDTableTopFriend.Application.Characters.Common;

public class CharacterResultMapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Character, CharacterResult>()
            .Map(dest => dest.Id, src => src.GetId().Value)
            .Map(dest => dest.UserId, src => src.UserId.Value)
            .Map(dest => dest.AudioEffectIds, src => src.AudioEffectIds.Select(userId => userId.Value))
            .Map(dest => dest.CharacterSheet, src => src.CharacterSheet.Adapt<CharacterSheetResult>())
            .MapToConstructor(true);

        config.NewConfig<CharacterSheet, CharacterSheetResult>()
            .Map(dest => dest.SkillIds, src => src.SkillIds.Select(skillId => skillId.Value))
            .Map(dest => dest.StatusIds, src => src.StatusIds.Select(statusId => statusId.Value));
    }
}
