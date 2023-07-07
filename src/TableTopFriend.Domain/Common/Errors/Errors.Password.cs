using ErrorOr;

namespace TableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Password
    {
        public static Error NullOrEmpty => Error.Validation("Password.NullOrEmpty", "The email cannot be null or empty");
        public static Error MinimumLength => Error.Validation("Password.MinimumLength", "The password provided is too short");
    }
}
