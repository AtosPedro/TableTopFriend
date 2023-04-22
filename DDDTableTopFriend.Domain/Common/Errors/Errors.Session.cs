using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Session 
    {
        public static Error NotScheduled => Error.NotFound("Session.NotScheduled", "Session not scheduled.");
        public static Error AlreadyScheduled => Error.Conflict("Session.AlreadyScheduled", "Session in the same date was already scheduled.");
    }
}
