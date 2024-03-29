using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.Common.ValueObjects;

public sealed class Mail : ValueObject
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;

    private Mail(
        string to,
        string subject,
        string body)
    {
        To = to;
        Subject = subject;
        Body = body;
    }

    public static Mail Create(
        string to,
        string subject,
        string body)
    {
        return new(to, subject, body);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return To;
        yield return Subject;
        yield return Body;
    }

    
#pragma warning disable CS8618
    private Mail()
    {
    }
#pragma warning restore CS8618
}
