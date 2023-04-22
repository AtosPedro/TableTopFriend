using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class AudioEffect
    {
        public static Error NotRegistered => Error.NotFound("AudioEffect.NotRegistered", "AudioEffect not registered.");
    }
}
