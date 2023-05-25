namespace TableTopFriend.Infrastructure.Persistence.Context;

public class ApplicationDbSettings
{
    public const string SectionName = "DBConfiguration";
    public string Server { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string Password { get; set; }= null!;
    public string Port { get; set; } = null!;
    public string Database { get; set; } = null!;
    public string SSLMode { get; set; } = null!;
    public bool AllowPublicKeyRetrieval { get; set; }
    public string ConnectionString { get; set; } = null!;
}
