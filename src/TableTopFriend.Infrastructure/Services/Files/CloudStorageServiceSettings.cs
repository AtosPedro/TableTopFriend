namespace TableTopFriend.Infrastructure.Services.Files;

public class CloudStorageServiceSettings
{
    public static string SectionName => "CloudStorageServiceSettings";
    public string ApiKey { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string BaseUrl { get; set; } = null!; 
    public string Bucket { get; set; } = null!; 
}
