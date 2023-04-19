using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Skill
    {
        public static Error NotRegistered => Error.NotFound("Skill.NotRegistered", "The skill is not registered.");
        public static Error AlreadyRegistered => Error.NotFound("Skill.AlreadyRegistered", "The skill is already registered.");
    }
}
