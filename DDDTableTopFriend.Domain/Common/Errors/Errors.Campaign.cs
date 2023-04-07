using ErrorOr;

namespace DDDTableTopFriend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Campaign
    {
        public static Error DuplicateName => Error.Conflict("Campaign.DuplicateName", "A campaign with this name already exists.");
        public static Error NotRegistered => Error.NotFound("Campaign.NotRegistered", "The campaign is not registered.");
    }
}
