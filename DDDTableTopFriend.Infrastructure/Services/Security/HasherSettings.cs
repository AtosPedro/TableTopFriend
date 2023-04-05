namespace DDDTableTopFriend.Infrastructure.Services.Security;

public class HasherSettings
{
    public const string SectionName = "HasherSettings";
    public int Iterations { get; init; }
    public string Pepper { get; init; } = null!;
}
