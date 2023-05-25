using ErrorOr;

namespace TableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error IncorrectPassword => Error.Validation("User.IncorrectPassword", "The given password was incorrect.");
        public static Error UserNotRegistered => Error.NotFound("User.UserNotRegistered", "User not registered.");
        public static Error UserAlreadyRegistered => Error.Conflict("User.UserAlreadyRegistered", "User already registered.");
    }
}
