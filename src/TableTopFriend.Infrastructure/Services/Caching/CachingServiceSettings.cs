namespace TableTopFriend.Infrastructure.Services.Caching;

public class CachingSettings
{
    public const string SectionName = "CachingSettings";
    public string ConnectionString { get; set; } = null!;
}
