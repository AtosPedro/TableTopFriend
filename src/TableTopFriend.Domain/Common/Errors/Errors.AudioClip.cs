using ErrorOr;

namespace TableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static partial class AudioClip
    {
        public static Error NullValue => Error.NotFound("AudioClip.NullValue", "AudioClip is null.");
    }
}
