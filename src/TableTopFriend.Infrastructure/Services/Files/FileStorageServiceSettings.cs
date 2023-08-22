namespace TableTopFriend.Infrastructure.Services.Files;

public class FileStorageServiceSettings
{
    public static string SectionName => "FileStorageServiceSettings";
    public string ApiKey { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string BaseUrl { get; set; } = null!; 
    public string Bucket { get; set; } = null!; 
}
