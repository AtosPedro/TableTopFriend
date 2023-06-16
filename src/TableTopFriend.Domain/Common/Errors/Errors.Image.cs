using ErrorOr;

namespace TableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Image
    {
        public static Error InvalidImage => Error.Validation("Image.InvalidImage", "Image provided was invalid.");
    }
}

