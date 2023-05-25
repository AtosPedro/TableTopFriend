using ErrorOr;

namespace TableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class YoutubeUrl
    {
        public static Error InvalidUrl => Error.Validation("YoutubeUrl.InvalidUrl", "The provided url is invalid");
    }
}
