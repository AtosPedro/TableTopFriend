using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Description
    {
        public static Error MaximumLength => Error.Validation("Name.MaximumLength", "The name is too long");
    }
}
