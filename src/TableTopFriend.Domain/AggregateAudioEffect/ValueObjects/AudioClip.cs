using TableTopFriend.Domain.Common.Models;
using ErrorOr;

namespace TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;

public sealed class AudioClip : ValueObject
{
    public byte[] Value { get; }

    private AudioClip(byte[] value) => Value = value;

    public static ErrorOr<AudioClip> Create(byte[] value)
    {
        return new AudioClip(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
