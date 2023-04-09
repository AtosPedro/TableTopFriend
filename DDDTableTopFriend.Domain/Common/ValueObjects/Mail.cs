using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.Common.ValueObjects;

public class Mail : ValueObject
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return To;
        yield return Subject;
        yield return Body;
    }
}
