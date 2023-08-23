using ErrorOr;

namespace TableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static partial class Updated
    {
        public static Error InvalidAt => Error.NotFound("Created.NotRegistered", "The date of alteration is invalid");
        public static Error NullOrEmptyBy => Error.NotFound("Created.NotRegistered", "The updated by is null or empty");
    }
}