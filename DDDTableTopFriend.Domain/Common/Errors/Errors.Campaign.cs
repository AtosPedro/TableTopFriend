using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Campaign
    {
        public static Error DuplicateName => Error.Conflict("Campaign.DuplicateEmail");
    }
}
