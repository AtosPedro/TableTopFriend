using ErrorOr;

namespace TableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Character
    {
        public static Error NotRegistered => Error.NotFound("Character.NotRegistered", "The character is not registered.");
    }
}
