using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;

namespace TableTopFriend.Domain.AggregateAudioEffect.ValueObjects;

public sealed class AudioClip : ValueObject
{
    public byte[] Value { get; }

    private AudioClip(byte[] value) => Value = value;

    public static ErrorOr<AudioClip> Create(byte[]? value)
    {
        if (value is null)
            return Errors.AudioClip.NullValue;

        return new AudioClip(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private AudioClip()
    {
    }
#pragma warning restore CS8618
}
