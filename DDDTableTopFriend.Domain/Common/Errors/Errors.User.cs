using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error InvalidId => Error.NotFound("User.InvalidId", "User Id provided was invalid.");
    }
}
