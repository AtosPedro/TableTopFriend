using TableTopFriend.Domain.AggregateCharacterInstance.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateCharacterInstance.Entities;

public sealed class CharacterSheetInstance : Entity<CharacterSheetInstanceId>
{
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;    
    public CharacterSheetInstance(CharacterSheetInstanceId id) : base(id){}
}