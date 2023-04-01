using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error IncorrectPassword => Error.Validation("User.IncorrectPassword", "The given password was incorrect.");
        public static Error UserNotRegistered => Error.Validation("User.UserNotRegistered", "User not registered.");
        public static Error UserAlreadyRegistered => Error.Validation("User.UserAlreadyRegistered", "User already registered.");
    }
}
