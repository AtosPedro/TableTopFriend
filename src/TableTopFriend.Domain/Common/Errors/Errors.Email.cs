using ErrorOr;

namespace TableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Email
    {
        public static Error NullOrEmpty => Error.Validation("Email.NullOrEmpty", "The email cannot be null or empty");
        public static Error InvalidEmail => Error.Validation("Email.InvalidEmail", "The email provided is invalid");
    }
}
