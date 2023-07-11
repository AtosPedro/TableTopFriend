using Microsoft.Extensions.Options;
using TableTopFriend.Application.Common.Interfaces.Services;

namespace TableTopFriend.Infrastructure.Services.Files;

public class FirebaseCloudStorageService : ICloudStorageService
{
    private readonly CloudStorageServiceSettings _options;

    public FirebaseCloudStorageService(IOptions<CloudStorageServiceSettings> options)
    {
        _options = options.Value;
    }

    public async Task SaveFileAsync(
         string fileName,
         Stream fileStream)
    {
        await Task.CompletedTask;
    }

    public async Task DeleteFileAsync(string fileName)
    {
        await Task.CompletedTask;
    }
}
