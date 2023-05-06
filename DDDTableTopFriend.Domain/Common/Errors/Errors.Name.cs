using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Name
    {
        public static Error NullOrEmpty => Error.Validation("Name.NullOrEmpty", "The name is null or empty");
        public static Error MinimumLength => Error.Validation("Name.MinimumLength", "The name is too short");
        public static Error MaximumLength => Error.Validation("Name.MaximumLength", "The name is too long");
    }
}
