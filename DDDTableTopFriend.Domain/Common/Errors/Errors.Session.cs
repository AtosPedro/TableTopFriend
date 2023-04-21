using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Session 
    {
        public static Error NotScheduled => Error.Validation("Session.NotScheduled", "Session not registered.");
    }
}
