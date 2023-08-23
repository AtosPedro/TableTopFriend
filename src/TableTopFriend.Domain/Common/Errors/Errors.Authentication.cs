using ErrorOr;

namespace TableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error WrongCredentials => Error.Validation("User.WrongCredentials", "The given credentials was incorrect.");
        public static Error UserNotRegistered => Error.NotFound("User.UserNotRegistered", "User not registered.");
        public static Error UserAlreadyRegistered => Error.Conflict("User.UserAlreadyRegistered", "User already registered.");
    }
}
