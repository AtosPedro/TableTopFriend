using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Status
    {
        public static Error NotRegistered => Error.NotFound("Status.NotRegistered", "The status is not registered.");
    }
}
