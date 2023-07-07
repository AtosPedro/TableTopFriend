using TableTopFriend.Domain.AggregateCharacterInstance.Entities;
using TableTopFriend.Domain.AggregateCharacterInstance.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.AggregateCharacter.Enums;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateCharacterInstance;

public sealed class CharacterInstance : AggregateRoot<CharacterInstanceId, Guid>
{
    public UserId UserId { get; set; } = null!;
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public byte[]? Image { get; private set; }
    public CharacterType Type { get; private set; }
    public CharacterSheetInstance CharacterSheet { get; private set; } = null!;
    public CharacterInstance(CharacterInstanceId id) : base(id){}
}