using ErrorOr;

namespace TableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static partial class Created
    {
        public static Error InvalidAt => Error.NotFound("Created.NotRegistered", "The date of creation is invalid");
        public static Error NullOrEmptyBy => Error.NotFound("Created.NotRegistered", "The created by is null or empty");
    }
}